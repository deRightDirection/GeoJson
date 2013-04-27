using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary.Tests
{
    public class TestEntityWithWrongSpatialType
    {
        [GeoJsonGeometry]
        public string Geometry { get;set;}
    }
}