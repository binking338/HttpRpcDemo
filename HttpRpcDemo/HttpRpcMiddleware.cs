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


namespace HttpRpcDemo
{
    public class HttpRpcMiddleware
    {

        public HttpRpcMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context, ServiceInvoker invoker, Serializer serializer)
        {
            var typeName = System.Web.HttpUtility.UrlDecode(context.Request.Headers["typeName"].FirstOrDefault());
            var methodName = System.Web.HttpUtility.UrlDecode(context.Request.Headers["methodName"].FirstOrDefault());
            var serializedParameters = new StreamReader(context.Request.Body).ReadToEndAsync().Result;

            var parameters = serializer.Deserialize(serializedParameters, typeof(object[])) as object[];
            var result = invoker.Call(context.RequestServices, typeName, methodName, parameters);
            var serializedResult = serializer.Serialize(result);
            await context.Response.WriteAsync(serializedResult);
        }
    }
}
