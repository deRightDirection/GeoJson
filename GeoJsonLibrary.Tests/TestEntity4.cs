using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary.Tests
{
    public class TestEntity4
    {
        public int Value { get; set; }
        public List<string> Values { get; set; }
        public string Name { get; set; }

        public int NotInUse { get; set; }

        public TestEntity2 SubEntity { get; set; }

    }
}