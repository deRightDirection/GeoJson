using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeoJsonLibrary
{
    internal class GeoJsonConverter
    {
        /// <summary>
        /// Converts an object or a list of objects to geojson representation
        /// </summary>
        /// <typeparam name="T">objecttype</typeparam>
        /// <param name="objectToConvert">an object or list of objects</param>
        /// <returns>geojson representation of the object or list of objects</returns>
        public GeoJsonFeatureCollection ConvertToGeoJsonFeatureCollection<T>(T objectToConvert)
        {
            GeoJsonFeatureCollection featureCollection = new GeoJsonFeatureCollection();
            var typeToConvert = objectToConvert.GetType();
            var typeIsFromList = GetTypeOfList(typeToConvert);
            if (typeIsFromList != null)
            {
                var list = objectToConvert as IEnumerable;
                foreach (var item in list)
                {
                    AddFeature(featureCollection, item);
                }
            }
            else
            {
                AddFeature(featureCollection, objectToConvert);
            }
            return featureCollection;
        }

        private void AddFeature(GeoJsonFeatureCollection featureCollection, object objectToConvert)
        {
            GeoJsonFeature feature = new GeoJsonFeature();
            var geometry = GetGeometry(objectToConvert);
            feature.Geometry =  new GeoJsonGeometry(geometry);
            feature.Properties = GetFeatureData(objectToConvert);
            featureCollection.Features.Add(feature);
        }

        private Type GetTypeOfList(Type objectToConvert)
        {
            foreach (Type interfaceType in objectToConvert.GetInterfaces())
            {
                var isGenericType = interfaceType.IsGenericType;
                var isIlistOrIEnumerable = interfaceType.GetGenericTypeDefinition() == typeof(IList<>) || interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                if (isGenericType && isIlistOrIEnumerable)
                {
                    return objectToConvert.GetGenericArguments()[0];
                }
            }
            return null;
        }

        public T ConvertFromGeoJson<T>(string geojson)
        {
            throw new NotImplementedException("not implemented in version 1.0 of this library");
            return default(T);
        }

        private  Dictionary<string, object> GetFeatureData(object objectToConvertToGeoJson)
        {
            var result = new Dictionary<string,object>();
            Type type = objectToConvertToGeoJson.GetType();
            List<string> propertyClassAtrributes = GetPropertyClassAttributes(type);
            foreach(var property in type.GetProperties())
            {
                var geoJsonPropertyAttribute = property.GetCustomAttribute<GeoJsonPropertyAttribute>();
                var name = property.Name.ToLowerInvariant();
                if (geoJsonPropertyAttribute != null)
                {
                    var value = property.GetValue(objectToConvertToGeoJson);
                    result.Add(name, value);
                }
                else
                {
                    if (propertyClassAtrributes.Contains(name))
                    {
                        var value = property.GetValue(objectToConvertToGeoJson);
                        result.Add(name, value);
                    }
                }
            }
            return result;
        }

        private List<string> GetPropertyClassAttributes(Type type)
        {
            List<string> result = new List<string>();
            var attributes = type.GetCustomAttributes<GeoJsonPropertyAttribute>(true);
            foreach (var attribute in attributes)
            {
                if (!string.IsNullOrEmpty(attribute.PropertyName))
                {
                    result.Add(attribute.PropertyName.ToLowerInvariant());
                }
            }
            return result;
        }

        private DbGeography GetGeometry(object objectToConvertToGeoJson)
        {
            Type type = objectToConvertToGeoJson.GetType();
            bool hasGeometryProperty = false;
            string geometryClassAttributePropertyName = GetGeometryClassAttributePropertyName(type);
            foreach (var property in type.GetProperties())
            {
                var geoJsonAttribute = property.GetCustomAttribute<GeoJsonGeometryAttribute>();
                if (geoJsonAttribute != null)
                {
                    if (property.PropertyType == typeof(DbGeography)) 
                    {
                        return property.GetValue(objectToConvertToGeoJson) as DbGeography;
                    }
                    // TODO: indien het geen primitief type is, alle properties van het type doorlopen en indien er niet primitieve typen
                    // TODO: tussen zitten de class voorzien van het attribuut dat alleen expliciet aangegeven members worden omgezet naar json
                    else
                    {
                        throw new GeoJsonException("Property with geometry is type of SqlGeography");
                    }
                }
                else
                {
                    if(!string.IsNullOrEmpty(geometryClassAttributePropertyName))
                    {
                        if(string.Equals(geometryClassAttributePropertyName, property.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return property.GetValue(objectToConvertToGeoJson) as DbGeography;
                        }
                    }
                }
            }
            if (!hasGeometryProperty)
            {
                throw new GeoJsonException("Object doesn't have geometry property with GeoJsonGeometry attribute");
            }
            return null;
        }

        private string GetGeometryClassAttributePropertyName(Type type)
        {
            var geoJsonAttribute = type.GetCustomAttribute<GeoJsonGeometryAttribute>();
            if (geoJsonAttribute != null)
            {
                return geoJsonAttribute.PropertyName;
            }
            return null;
        }
    }
}