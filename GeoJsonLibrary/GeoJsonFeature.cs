using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data.Entity.Spatial;

namespace GeoJsonLibrary
{
    public class GeoJsonFeature
    {
        [JsonProperty("geometry")]
        [JsonConverter(typeof(DbGeographyGeoJsonConverter))]
        public DbGeography Geometry { get; internal set; }

        [JsonProperty("type")]
        public string Type { get { return "Feature"; } }

        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; internal set; }
    }
}