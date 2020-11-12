using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkAPI.Objects;
using ParkAPI.Settings;
using UnityEngine;

namespace ParkAPI
{
     public interface IParkProvider
     {
          Task<List<Place>> GetNearbyParks(ParkRequestSettings settings);
     }

}