using Mapbox.Utils;

namespace OurWorld.Scripts.Extensions
{
    public static class GeExtensions
    {
        public static Vector2d Reverse(this Vector2d vector)
        {
            return new Vector2d(vector.y,vector.x);
        }
    }
}