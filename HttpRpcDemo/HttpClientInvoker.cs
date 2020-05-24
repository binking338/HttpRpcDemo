using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace HttpRpcDemo
{
    public class HttpClientInvoker
    {
        private Serializer serializer;
        private IHttpClientFactory httpClientFactory;
        public HttpClientInvoker(Serializer serializer, IHttpClientFactory httpClientFactory)
        {
            this.serializer = serializer;
            this.httpClientFactory = httpClientFactory;
        }

        public object Call(MethodInfo serviceMethodInfo, object[] parameters)
        {
            var serviceType = serviceMethodInfo.DeclaringType;
            var returnType = serviceMethodInfo.ReturnType;
            var serializedParameters = serializer.Serialize(parameters);
            var httpClient = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, UrlMap[serviceType]);
            request.Headers.Add("typeName", System.Web.HttpUtility.UrlEncode(serviceType.AssemblyQualifiedName));
            request.Headers.Add("methodName", System.Web.HttpUtility.UrlEncode(serviceMethodInfo.Name));
            request.Content = new StringContent(serializedParameters);

            var result = httpClient.SendAsync(request)
                .ContinueWith(sendTask => {
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

        internal static Dictionary<Type, string> UrlMap = new Dictionary<Type, string>();
    }
}
