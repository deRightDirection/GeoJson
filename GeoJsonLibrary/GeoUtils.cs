using System;
using System.Collections.Generic;
using System.Data.Spatial;
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
            var text = string.Format("POINT({0} {1})", longitude, latitude);
            return DbGeography.PointFromText(text, 4326);
        }
    }
}