using Managers;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Linq;

public class GetNearbyParks : MonoBehaviour
{
     string apiKey = "AIzaSyAw4JyCQ677K3hlOq94apInAK6To-OjUHs";

     private async void Start()
     {
         var location = new GeoLocation
          {
               Lat = 39.996117,
               Lng = 32.707689
          };
          LocationAPIManager manager = new GoogleLocationAPIProvider(apiKey);

          var places = await manager.GetNearbyParks(new GoogleParkRequestSettings(1000, location));

          places.ForEach(x => { Debug.LogWarning(x.Name); });

     }
}
    