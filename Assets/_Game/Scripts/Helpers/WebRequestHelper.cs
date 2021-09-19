using System;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.Interfaces;
using OurWorld.Scripts.Interfaces.Serialization;
using UnityEngine.Networking;

namespace OurWorld.Scripts.Helpers
{
    public class WebRequestHelper : IWebRequestHelper
    {
        private readonly ISerializationOption _serializationOptions;
        public WebRequestHelper(ISerializationOption serializationOptions)
        {
            _serializationOptions = serializationOptions;
        }
        public async UniTask<RequestResponse<T>> GetAsync<T>(string url)
        {
            return await InternalGetAsync<T>(new Uri(url));
        }
        public async UniTask<RequestResponse<T>> GetAsync<T>(Uri uri) => await InternalGetAsync<T>(uri);
        private async UniTask<RequestResponse<T>> InternalGetAsync<T>(Uri uri)
        {
            using var request = UnityWebRequest.Get(uri);

            request.SetRequestHeader("Content-Type", _serializationOptions.ContentType);

            var asyncOperation = await request.SendWebRequest();

            if (asyncOperation.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Get request failed. Error : {asyncOperation.error}");
                return new RequestResponse<T>(false,asyncOperation.error,default);
            }

            var result = _serializationOptions.Deserialize<T>(asyncOperation.downloadHandler.text);

            return new RequestResponse<T>(true, "", result);
        }

        public async UniTask<RequestResponse<string>> PostAsync<T>(string url,string data)
        {
            return await InternalPost(new Uri(url),data);
        }
        public async UniTask<RequestResponse<string>> PostAsync<T>(Uri uri,string data)
        {
            return await InternalPost(uri,data);
        }

        private async UniTask<RequestResponse<string>> InternalPost(Uri uri,string data){

            using var request = UnityWebRequest.Post(uri,data);

            request.SetRequestHeader("Content-Type","application/json");

            var asyncOperation = await request.SendWebRequest();

            if(asyncOperation.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Post request failed. Error : {asyncOperation.error}");
                return new RequestResponse<string>(false,asyncOperation.error,"");
            }

            return new RequestResponse<string>(true,"",asyncOperation.downloadHandler.text);
        }
    }

    public class RequestResponse<T>
    {
        public readonly bool Success;

        public readonly string ErrorMessage;

        public readonly T Data;

        public RequestResponse(bool success, string errorMessage, T data)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}