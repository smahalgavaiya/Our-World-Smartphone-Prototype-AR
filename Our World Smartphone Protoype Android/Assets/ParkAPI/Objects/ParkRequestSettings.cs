namespace Managers
{
     public class ParkRequestSettings
     {
          /// <summary>
          /// How many meters perimeter to be searched
          /// </summary>
          public int Radius { get; set; }

          /// <summary>
          /// Base location value
          /// </summary>
          public GeoLocation Location { get; set; }

          public ParkRequestSettings(int radius,GeoLocation location)
          {
               Radius = radius;
               Location = location;
          }
     }
}
