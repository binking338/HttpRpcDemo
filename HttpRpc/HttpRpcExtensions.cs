using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpRpc
{
    /// <summary>
    /// ASP.NET Core Style 扩展方法
    /// </summary>
    public static class HttpRpcExtensions
    {
        /// <summary>
        /// 添加服务端Rpc服务实现到DI容器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpRpcServer(this IServiceCollection services)
        {
            services.TryAddSingleton<IServiceInvoker, ServiceInvoker>();
            services.TryAddSingleton<ISerializer, Serializer>();
            return services;
        }

        /// <summary>
        /// 注册远程服务地址，并添加客户端Rpc服务实现到DI容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="url">目标地址</param>
        /// <param name="clientTypes">接口类型</param>
        /// <returns></returns>
        public static IServiceCollection AddHttpRpcClient(this IServiceCollection services, string url, params Type[] clientTypes)
        {
            services.AddHttpClient();
            services.TryAddSingleton<ISerializer, Serializer>();
            services.TryAddSingleton<IRemoteServiceInvoker, HttpClientInvoker>();
            services.TryAddSingleton<IRemoteServiceProxyGenerator, DynamicProxy.CastleCoreRemoteServiceProxyGenerator>();
            foreach(var clientType in clientTypes)
            {
                DynamicProxy.CastleCoreHttpRpcClientInterceptor.UrlMap[clientType] = url;
                var proxyType = clientType;
                services.AddSingleton(proxyType, serviceProvider => {
                    var generator = serviceProvider.GetService<IRemoteServiceProxyGenerator>();
                    var proxy = generator.CreateProxy(serviceProvider, proxyType);
                    return proxy;
                });
            }
            return services;
        }

        /// <summary>
        /// 添加服务端Rpc中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path">中间件路径</param>
        /// <returns></returns>
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
