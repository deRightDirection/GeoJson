using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GeoJsonLibrary.Tests
{
    public class TestEntity2
    {
        public string Name { get { return "TestEntity2"; } }
        public int Value { get { return 100; } }
        [JsonIgnore]
        public bool NotInUse { get;set;}
    }
}