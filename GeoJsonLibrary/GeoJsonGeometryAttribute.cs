using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple= false, Inherited = true)]
    public class GeoJsonGeometryAttribute : Attribute
    {
        public GeoJsonGeometryAttribute(string propertyNameWithSqlGeographyAsTypeDefinition = "")
        {
            PropertyName = propertyNameWithSqlGeographyAsTypeDefinition;
        }

        public string PropertyName { get; private set; }
    }
}