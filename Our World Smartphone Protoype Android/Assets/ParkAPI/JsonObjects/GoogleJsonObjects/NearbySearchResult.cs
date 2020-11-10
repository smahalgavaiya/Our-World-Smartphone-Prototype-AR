using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using System.Text;

namespace JsonModel
{
     public partial class NearbySearchResult
     {
          [JsonProperty("html_attributions")]
          public List<string> HtmlAttributions { get; set; }

          [JsonProperty("results")]
          public List<PlaceData> Places { get; set; }

          [JsonProperty("status")]
          public string Status { get; set; }
     }

     public partial class PlaceData
     {
          [JsonProperty("business_status")]
          public string BusinessStatus { get; set; }

          [JsonProperty("geometry")]
          public Geometry Geometry { get; set; }

          [JsonProperty("icon")]
          public Uri Icon { get; set; }

          [JsonProperty("name")]
          public string Name { get; set; }

          [JsonProperty("place_id")]
          public string PlaceId { get; set; }

          [JsonProperty("plus_code")]
          public PlusCode PlusCode { get; set; }

          [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
          public double? Rating { get; set; }

          [JsonProperty("reference")]
          public string Reference { get; set; }

          [JsonProperty("scope")]
          public string Scope { get; set; }

          [JsonProperty("types")]

          public List<TypeElement> Types { get; set; }

          [JsonProperty("user_ratings_total", NullValueHandling = NullValueHandling.Ignore)]
          public long? UserRatingsTotal { get; set; }

          [JsonProperty("vicinity")]
          public string Vicinity { get; set; }

          [JsonProperty("photos", NullValueHandling = NullValueHandling.Ignore)]
          public List<Photo> Photos { get; set; }
     }

     public partial class Geometry
     {
          [JsonProperty("location")]
          public Location Location { get; set; }

          [JsonProperty("viewport")]
          public Viewport Viewport { get; set; }
     }

     public partial class Location
     {
          [JsonProperty("lat")]
          public double Lat { get; set; }

          [JsonProperty("lng")]
          public double Lng { get; set; }
          public override string ToString()
          {
               StringBuilder sb = new StringBuilder();
               sb.Append(Lat.ToString().Replace(',', '.')).Append(',').Append(Lng.ToString().Replace(',', '.'));
               return sb.ToString();
          }
     }

     public partial class Viewport
     {
          [JsonProperty("northeast")]
          public Location Northeast { get; set; }

          [JsonProperty("southwest")]
          public Location Southwest { get; set; }
     }

     public partial class Photo
     {
          [JsonProperty("height")]
          public long Height { get; set; }

          [JsonProperty("html_attributions")]
          public List<string> HtmlAttributions { get; set; }

          [JsonProperty("photo_reference")]
          public string PhotoReference { get; set; }

          [JsonProperty("width")]
          public long Width { get; set; }
     }

     public partial class PlusCode
     {
          [JsonProperty("compound_code")]
          public string CompoundCode { get; set; }

          [JsonProperty("global_code")]
          public string GlobalCode { get; set; }
     }
     [JsonConverter(typeof(TypeElementConverter))]
     public enum TypeElement { Establishment, Park, PointOfInterest };

     internal static class Converter
     {
          public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
          {
               MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
               DateParseHandling = DateParseHandling.None,
               Converters =
            {
                TypeElementConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
          };
     }

     internal class TypeElementConverter : JsonConverter
     {
          public override bool CanConvert(Type t) => t == typeof(TypeElement) || t == typeof(TypeElement?);

          public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
          {
               if (reader.TokenType == JsonToken.Null) return null;
               var value = serializer.Deserialize<string>(reader);
               switch (value)
               {
                    case "establishment":
                         return TypeElement.Establishment;
                    case "park":
                         return TypeElement.Park;
                    case "point_of_interest":
                         return TypeElement.PointOfInterest;
               }
               throw new Exception("Cannot unmarshal type TypeElement");
          }

          public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
          {
               if (untypedValue == null)
               {
                    serializer.Serialize(writer, null);
                    return;
               }
               var value = (TypeElement)untypedValue;
               switch (value)
               {
                    case TypeElement.Establishment:
                         serializer.Serialize(writer, "establishment");
                         return;
                    case TypeElement.Park:
                         serializer.Serialize(writer, "park");
                         return;
                    case TypeElement.PointOfInterest:
                         serializer.Serialize(writer, "point_of_interest");
                         return;
               }
               throw new Exception("Cannot marshal type TypeElement");
          }

          public static readonly TypeElementConverter Singleton = new TypeElementConverter();
     }
}

