using Cysharp.Threading.Tasks;
using JsonModel;
using Newtonsoft.Json;
using ParkAPI.Objects;
using ParkAPI.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;

namespace ParkAPI
{
    public class GoogleLocationAPIProvider : LocationAPIManager
     {

          public GoogleLocationAPIProvider(string apiKey):base(apiKey)
          {
               // google specific setting here
          }
          public override async Task<List<Place>> GetNearbyParks(ParkRequestSettings settings)
          {
               List<Place> places = new List<Place>();
               NameValueCollection queryParameters = new NameValueCollection();
               queryParameters.Add("key", APIKey);
               queryParameters.Add("type", "park");
               queryParameters.Add("location", settings.Location.ToString());
               queryParameters.Add("radius", settings.Radius.ToString());
               Uri uri = new Uri("https://maps.googleapis.com/maps/api/place/nearbysearch/json?"+ToQueryString(queryParameters));

               var request = await UnityWebRequest.Get(uri.AbsoluteUri)
                   .SendWebRequest();
               Debug.Log(request.downloadHandler.text);
               var searchResult = JsonConvert.DeserializeObject<NearbySearchResult>(request.downloadHandler.text);
               foreach (var item in searchResult.Places)
               {
                    places.Add(new Place
                    {
                         Name = item.Name,
                         Location = new GeoLocation
                         {
                              Lat = item.Geometry.Location.Lat,
                              Lng = item.Geometry.Location.Lng
                         },
                         PlaceID=item.PlaceId,
                    });
               }
               return places;
          }


          private string ToQueryString(NameValueCollection nvc)
          {
               var array = (
                   from key in nvc.AllKeys
                   from value in nvc.GetValues(key)
                   select string.Format(
            "{0}={1}",
            WebUtility.UrlEncode(key),
            WebUtility.UrlEncode(value))
                   ).ToArray();
               return  string.Join("&", array);
          }
     }
}
