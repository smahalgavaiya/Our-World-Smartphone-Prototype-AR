using ParkAPI.Objects;
using ParkAPI.Settings;

namespace ParkAPI.Settings
{
    public class GoogleParkRequestSettings : ParkRequestSettings
     {
          public GoogleParkRequestSettings(int radius, GeoLocation location) : base(radius, location)
          {
          }
     }
}
