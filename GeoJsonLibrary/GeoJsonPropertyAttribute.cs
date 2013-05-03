using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class GeoJsonPropertyAttribute : Attribute
    {
        public GeoJsonPropertyAttribute()
        {
            PropertyName = string.Empty;
            JsonPropertyName = string.Empty;
        }

        public string JsonPropertyName { get; set; }

        public string PropertyName { get; set; }
    }
}