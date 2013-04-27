using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJsonLibrary
{
    public class GeoJsonException : Exception
    {
        public GeoJsonException(string message) : base(message)
        {

        }
    }
}