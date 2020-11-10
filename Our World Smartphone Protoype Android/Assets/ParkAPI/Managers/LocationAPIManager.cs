using Interfaces;
using Objects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
     public abstract class LocationAPIManager:IParkProvider
     {

          public string APIKey { get; set; }

          public LocationAPIManager(string apiKey)
          {
               APIKey = apiKey;
          }

          public abstract Task<List<Place>> GetNearbyParks(ParkRequestSettings settings);
     } 
}
