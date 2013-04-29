using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GeoJsonLibrary
{
    public class GeoJsonGeometry
    {
        private DbGeography _geometry;

        public GeoJsonGeometry(DbGeography geometry)
        {
            _geometry = geometry;
            ReadCoordinates();
        }

        private void ReadCoordinates()
        {
            Latitude = (double)_geometry.Latitude;
            Longitude = (double)_geometry.Longitude;
        }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get { return new List<double>() { Latitude, Longitude }.ToArray(); } }

        [JsonProperty("type")]
        public string Type { get { return "Point"; } }

        [JsonIgnore]
        public double Latitude { get; private set; }

        [JsonIgnore]
        public double Longitude { get; private set; }
    }
}