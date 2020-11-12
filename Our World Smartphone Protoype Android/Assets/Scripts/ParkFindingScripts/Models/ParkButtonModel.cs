using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;

namespace ParkFindingScripts.Models
{
    public class ParkButtonModel
    {
        public ParkButtonModel(string parkName, string placeID, LatLong location)
        {
            this.ParkName = parkName;
            this.PlaceID = placeID;
            this.Location = location;

        }
        public string ParkName { get; set; }

        public string PlaceID { get; set; }

        public LatLong Location { get; set; }
        



        }
    }
