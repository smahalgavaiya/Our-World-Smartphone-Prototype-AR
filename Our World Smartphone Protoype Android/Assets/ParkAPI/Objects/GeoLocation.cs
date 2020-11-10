using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GeoLocation
{
     public double Lat { get; set; }

     public double Lng { get; set; }
     public override string ToString()
     {
          StringBuilder sb = new StringBuilder();
          sb.Append(Lat.ToString().Replace(',', '.')).Append(',').Append(Lng.ToString().Replace(',', '.'));
          return sb.ToString();
     }
}
