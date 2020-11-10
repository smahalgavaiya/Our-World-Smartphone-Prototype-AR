using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
     public class Place
     {
          public string Name { get; set; }

          public GeoLocation Location { get; set; }

          public Distance Distance { get; set; }

          public Duration Duration { get; set; }
     }
}
