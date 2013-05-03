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
        public T ConvertFromGeoJson<T>(string geojson)
        {
            throw new NotImplementedException("not implemented in version 1.0 of this library");
            return default(T);
        }

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
            feature.Geometry = new GeoJsonGeometry(geometry);
            feature.Properties = GetFeatureData(objectToConvert);
            featureCollection.Features.Add(feature);
        }

        private Dictionary<string, object> GetFeatureData(object objectToConvertToGeoJson)
        {
            var result = new Dictionary<string, object>();
            Type type = objectToConvertToGeoJson.GetType();
            var propertyClassAtrributes = GetPropertyClassAttributes(type);
            foreach (var property in type.GetProperties())
            {
                var propertyName = property.Name.ToLowerInvariant();
                var geoJsonPropertyAttribute = property.GetCustomAttribute<GeoJsonPropertyAttribute>();
                var geoJsonClassPropertyAttribute = propertyClassAtrributes.Where(a => a.PropertyName.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (geoJsonPropertyAttribute != null || geoJsonClassPropertyAttribute != null)
                {
                    var name = GetPropertyName(property, geoJsonPropertyAttribute);
                    if (name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        name = GetPropertyName(property, geoJsonClassPropertyAttribute);
                    }
                    var value = property.GetValue(objectToConvertToGeoJson);
                    result.Add(name, value);
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
                    if (!string.IsNullOrEmpty(geometryClassAttributePropertyName))
                    {
                        if (string.Equals(geometryClassAttributePropertyName, property.Name, StringComparison.InvariantCultureIgnoreCase))
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

        private List<GeoJsonPropertyAttribute> GetPropertyClassAttributes(Type type)
        {
            List<GeoJsonPropertyAttribute> result = new List<GeoJsonPropertyAttribute>();
            var attributes = type.GetCustomAttributes<GeoJsonPropertyAttribute>(true);
            foreach (var attribute in attributes)
            {
                if (!string.IsNullOrEmpty(attribute.PropertyName))
                {
                    result.Add(attribute);
                }
            }
            return result;
        }

        private string GetPropertyName(PropertyInfo property, GeoJsonPropertyAttribute geoJsonPropertyAttribute)
        {
            var name = property.Name.ToLowerInvariant();
            if (geoJsonPropertyAttribute != null)
            {
                if (!string.IsNullOrEmpty(geoJsonPropertyAttribute.PropertyName))
                {
                    name = geoJsonPropertyAttribute.PropertyName.ToLowerInvariant();
                }
                if (!string.IsNullOrEmpty(geoJsonPropertyAttribute.JsonPropertyName))
                {
                    name = geoJsonPropertyAttribute.JsonPropertyName.ToLowerInvariant();
                }
            }
            return name;
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
    }
}