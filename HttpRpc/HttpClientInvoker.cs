using System;
using System.Net.Http;
using System.Reflection;

namespace HttpRpc
{
    /// <summary>
    /// 基于HTTP的远程服务调用器
    /// </summary>
    public class HttpClientInvoker : IRemoteServiceInvoker
    {
        public const string HEADER_SERVICE_NAME = "rpc-service-name";
        public const string HEADER_METHOD_NAME = "rpc-method-name";

        private ISerializer serializer;
        private IHttpClientFactory httpClientFactory;

        public HttpClientInvoker(ISerializer serializer, IHttpClientFactory httpClientFactory)
        {
            this.serializer = serializer;
            this.httpClientFactory = httpClientFactory;
        }

        public object Call(string url, MethodInfo serviceMethodInfo, object[] parameters)
        {
            var serviceType = serviceMethodInfo.DeclaringType;
            var returnType = serviceMethodInfo.ReturnType;
            var serializedParameters = serializer.Serialize(parameters);
            var httpClient = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add(HEADER_SERVICE_NAME, System.Web.HttpUtility.UrlEncode(serviceType.AssemblyQualifiedName));
            request.Headers.Add(HEADER_METHOD_NAME, System.Web.HttpUtility.UrlEncode(serviceMethodInfo.Name));
            request.Content = new StringContent(serializedParameters);

            var result = httpClient.SendAsync(request)
                .ContinueWith(sendTask =>
                {
                    if (sendTask.IsFaulted)
                    {
                        throw sendTask.Exception;
                    }
                    if (sendTask.IsCanceled)
                    {
                        throw new Exception("rpc canceled");
                    }
                    var httpResponseMessage = sendTask.Result;

                    var jsonResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    var result = serializer.Deserialize(jsonResult, returnType);
                    return result;
                }).Result;
            return result;
        }
    }
}
