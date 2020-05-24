using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpRpcDemo
{
    public static class HttpRpcExtensions
    {

        public static IServiceCollection AddHttpRpcServer(this IServiceCollection services)
        {
            services.TryAddSingleton<DIInvoker>();
            services.TryAddSingleton<Serializer>();
            return services;
        }

        public static IServiceCollection AddHttpRpcClient(this IServiceCollection services, string url, params Type[] clientTypes)
        {
            services.AddHttpClient();
            services.TryAddSingleton<Serializer>();
            services.TryAddSingleton<HttpClientInvoker>();
            services.TryAddSingleton<DynamicProxy.CastleCoreRemoteServiceProxyGenerator>();
            foreach(var clientType in clientTypes)
            {
                HttpClientInvoker.UrlMap[clientType] = url;
                var proxyType = clientType;
                services.AddSingleton(proxyType, serviceProvider => {
                    var generator = serviceProvider.GetService<DynamicProxy.CastleCoreRemoteServiceProxyGenerator>();
                    var proxy = generator.CreateProxy(serviceProvider, proxyType);
                    return proxy;
                });
            }
            return services;
        }

        public static IApplicationBuilder UseHttpRpc(this IApplicationBuilder app, string path = "/rpc")
        {
            app.Map(path, appBuilder =>
            {
                appBuilder.UseMiddleware<HttpRpcMiddleware>();
            });
            return app;
        }

    }
}
