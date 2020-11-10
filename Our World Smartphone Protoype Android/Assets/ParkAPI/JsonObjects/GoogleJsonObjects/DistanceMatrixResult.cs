using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonModel
{
     public partial class DistanceMatrixResult
     {
          [JsonProperty("destination_addresses")]
          public List<string> DestinationAddresses { get; set; }

          [JsonProperty("origin_addresses")]
          public List<string> OriginAddresses { get; set; }

          [JsonProperty("rows")]
          public List<Row> Rows { get; set; }

          [JsonProperty("status")]
          public string Status { get; set; }
     }

     public partial class Row
     {
          [JsonProperty("elements")]
          public List<Element> Elements { get; set; }
     }

     public partial class Element
     {
          [JsonProperty("distance")]
          public Distance Distance { get; set; }

          [JsonProperty("duration")]
          public Duration Duration { get; set; }

          [JsonProperty("status")]
          public string Status { get; set; }
     }

     public partial class Distance
     {
          [JsonProperty("text")]
          public string Text { get; set; }

          [JsonProperty("value")]
          public long Value { get; set; }
     }
     public partial class Duration
     {
          [JsonProperty("text")]
          public string Text { get; set; }

          [JsonProperty("value")]
          public long Value { get; set; }
     }
}