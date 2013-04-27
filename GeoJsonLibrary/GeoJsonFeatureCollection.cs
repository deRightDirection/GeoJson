using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeoJsonLibrary
{
    public class GeoJsonFeatureCollection
    {
        private List<GeoJsonFeature> _features;
        private GeoJsonConverter _converter;

        //!+ caching for type definitions 
        public GeoJsonFeatureCollection()
        {
            _features = new List<GeoJsonFeature>();
            _converter = new GeoJsonConverter();
        }

        [JsonProperty("type")]
        public string Type { get { return "FeatureCollection"; } }

        [JsonProperty("features")]
        public List<GeoJsonFeature> Features { get { return _features; } }

        public void AddFeatures<T>(T featuresToAdd)
        {
            var featureCollection = _converter.ConvertToGeoJsonFeatureCollection(featuresToAdd);
            _features = _features.Concat(featureCollection.Features).ToList();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}