using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;

namespace GeoJsonLibrary.Tests
{
    [GeoJsonProperty("Value")]
    [GeoJsonGeometry("Geometry")]
    public class TestEntity
    {
        public SqlGeography Geometry { get; set; }

        public int Value { get; set; }

        [GeoJsonProperty]
        public List<string> Values { get; set; }

        [GeoJsonProperty]
        public string Name { get; set; }

        public int NotInUse { get; set; }

        [GeoJsonProperty]
        public TestEntity2 SubEntity { get; set; }

        [GeoJsonProperty]
        public List<TestEntity4> SubGeometries { get; set; }
    }
}