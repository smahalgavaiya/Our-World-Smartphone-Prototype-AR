using System;
using System.Globalization;

namespace OurWorld.Scripts.DataModels.Geolocation
{
    public class BoundingBox
    {
        public Geolocation MinPoint { get; set; }
        public Geolocation MaxPoint { get; set; }

        public override string ToString()
        {
               return $"{MinPoint.ToString()},{MaxPoint.ToString()}";
        }
    }
}