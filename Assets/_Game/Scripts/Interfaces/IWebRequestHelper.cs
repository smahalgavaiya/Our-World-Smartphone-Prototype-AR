using System;
using Cysharp.Threading.Tasks;
using OurWorld.Scripts.Helpers;

namespace OurWorld.Scripts.Interfaces
{
    public interface IWebRequestHelper
    {
        UniTask<RequestResponse<T>> GetAsync<T>(string url);
        UniTask<RequestResponse<T>> GetAsync<T>(Uri uri);
        UniTask<RequestResponse<string>> PostAsync<T>(string url, string data);

        UniTask<RequestResponse<string>> PostAsync<T>(Uri uri, string data);
    }
}