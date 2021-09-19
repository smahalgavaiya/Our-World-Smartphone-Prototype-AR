using System;
using System.Globalization;

namespace OurWorld.Scripts.DataModels.Geolocation
{
    public class BoundingBox
    {
        public MapPoint MinPoint { get; set; }
        public MapPoint MaxPoint { get; set; }

        public override string ToString()
        {
               return $"{MinPoint.ToString()},{MaxPoint.ToString()}";
        }
    }
}