using Managers;
using Objects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
     public interface IParkProvider
     {
          Task<List<Place>> GetNearbyParks(ParkRequestSettings settings);
     }

}