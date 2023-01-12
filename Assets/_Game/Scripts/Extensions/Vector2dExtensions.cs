using Mapbox.Utils;
using OurWorld.Scripts.DataModels.GeolocationData;

namespace OurWorld.Scripts.Extensions
{
    public static class Vector2dExtensions
    {
        public static Vector2d Reverse(this Vector2d vector)
        {
            return new Vector2d(vector.y, vector.x);
        }

    }
}