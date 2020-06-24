using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;


namespace HttpRpc
{
    public class HttpRpcMiddleware
    {

        public HttpRpcMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context, IServiceInvoker invoker, ISerializer serializer)
        {
            var typeName = System.Web.HttpUtility.UrlDecode(context.Request.Headers[HttpClientInvoker.HEADER_SERVICE_NAME].FirstOrDefault());
            var methodName = System.Web.HttpUtility.UrlDecode(context.Request.Headers[HttpClientInvoker.HEADER_METHOD_NAME].FirstOrDefault());
            var serializedParameters = new StreamReader(context.Request.Body).ReadToEndAsync().Result;
            var targetType = Type.GetType(typeName);
            var methodInfo = targetType.GetMethod(methodName);

            var parameters = serializer.Deserialize(serializedParameters, typeof(object[])) as object[];
            var result = invoker.Call(context.RequestServices, methodInfo, parameters);
            var serializedResult = serializer.Serialize(result);
            await context.Response.WriteAsync(serializedResult);
        }
    }
}
