using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoJsonLibrary
{
    //! TODO: overzetten naar MannusLibrary
    public static class GeoUtils
    {
        /// <summary>
        /// Create a GeoLocation point based on latitude and longitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            string lat = latitude.ToString(CultureInfo.InvariantCulture);
            string lon = longitude.ToString(CultureInfo.InvariantCulture);
            var text = string.Format("POINT({0} {1})", lon,lat);
            return DbGeography.PointFromText(text, 4326);
        }
    }
}