using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;

namespace GeoJsonLibrary
{
    public class GeoJsonFeature
    {
        [JsonProperty("geometry")]
        public GeoJsonGeometry Geometry { get; internal set; }

        [JsonProperty("type")]
        public string Type { get { return "Feature"; } }

        [JsonProperty("properties")]
        public Dictionary<string, object> Properties { get; internal set; }
    }
}