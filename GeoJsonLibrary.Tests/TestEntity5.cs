using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary.Tests
{
    [GeoJsonProperty(PropertyName = "Value", JsonPropertyName = "Waarde")]
    [GeoJsonGeometry("Geometry")]
    public class TestEntity5
    {
        public DbGeography Geometry { get; set; }

        public int Value { get; set; }

        [GeoJsonProperty(JsonPropertyName = "Getal")]
        public int Value2 { get; set; }
    }
}