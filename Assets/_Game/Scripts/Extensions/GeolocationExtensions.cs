using System;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Extensions
{
    public static class GeolocationExtensions
    {
        // Semi-axes of WGS-84 geoidal reference
        private const double WGS84_a = 6378137.0; // Major semiaxis [m]
        private const double WGS84_b = 6356752.3; // Minor semiaxis [m]

        // 'halfSideInKm' is the half length of the bounding box you want in kilometers.
        public static BoundingBox GetBoundingBox(this Geolocation point, double halfSideInKm)
        {
            // Bounding box surrounding the point at given coordinates,
            // assuming local approximation of Earth surface as a sphere
            // of radius given by WGS84
            var lat = Deg2rad(point.Latitude);
            var lon = Deg2rad(point.Longitude);
            var halfSide = 1000 * halfSideInKm;

            // Radius of Earth at given latitude
            var radius = WGS84EarthRadius(lat);
            // Radius of the parallel at given latitude
            var pradius = radius * Math.Cos(lat);

            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / pradius;
            var lonMax = lon + halfSide / pradius;

            return new BoundingBox
            {
                MinPoint = new Geolocation { Latitude = Rad2deg(latMin), Longitude = Rad2deg(lonMin) },
                MaxPoint = new Geolocation { Latitude = Rad2deg(latMax), Longitude = Rad2deg(lonMax) }
            };
        }

        public static double[] ToLatLonArray(this Geolocation geolocation) => new[] { geolocation.Latitude, geolocation.Longitude };

        public static double[] ToLonLatArray(this Geolocation geolocation) => new[] { geolocation.Longitude, geolocation.Latitude };

        public static Geolocation ToRadianGeolocation(this Geolocation geolocation)
        {
            geolocation.Latitude = Deg2rad(geolocation.Latitude);
            geolocation.Longitude = Deg2rad(geolocation.Longitude);
            return geolocation;
        }

        /// <summary>
        /// Returns direct distance between 2 points on earth in kilometers
        /// </summary>
        public static float DistanceTo(this Geolocation rh, Geolocation lh)
        {
            rh = rh.ToRadianGeolocation();
            lh = lh.ToRadianGeolocation();

            double distanceLongtitue = lh.Longitude - rh.Longitude;
            double distanceLatitude = lh.Latitude - rh.Latitude;

            double haversine = Math.Pow(Math.Sin(distanceLatitude / 2), 2) +
                Math.Cos(rh.Latitude) *
                Math.Cos(lh.Latitude) *
                Math.Pow(Math.Sin(distanceLongtitue / 2), 2);

            double rawDistance = 2 * Math.Asin(Math.Sqrt(haversine));

            return (float)(rawDistance * 6371f);
        }

        // degrees to radians
        private static double Deg2rad(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        // radians to degrees
        private static double Rad2deg(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        // Earth radius at a given latitude, according to the WGS-84 ellipsoid [m]
        private static double WGS84EarthRadius(double lat)
        {
            // http://en.wikipedia.org/wiki/Earth_radius
            var An = WGS84_a * WGS84_a * Math.Cos(lat);
            var Bn = WGS84_b * WGS84_b * Math.Sin(lat);
            var Ad = WGS84_a * Math.Cos(lat);
            var Bd = WGS84_b * Math.Sin(lat);
            return Math.Sqrt((An * An + Bn * Bn) / (Ad * Ad + Bd * Bd));
        }
    }
}
