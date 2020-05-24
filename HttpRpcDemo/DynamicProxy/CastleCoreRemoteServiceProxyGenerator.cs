using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpRpcDemo.DynamicProxy
{
    public class CastleCoreRemoteServiceProxyGenerator
    {
        internal static ProxyGenerator ProxyGenerator { get; set; } = new ProxyGenerator();

        public object CreateProxy(IServiceProvider serviceProvider, Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            object proxy = null;
            try
            {
                var interceptors = new List<IInterceptor>();
                interceptors.Add(new CastleCoreHttpRpcClientInterceptor(serviceProvider.GetService<HttpClientInvoker>()));
                if (serviceType.IsClass)
                {
                    var ctors = serviceType.GetConstructors();
                    if (ctors.Count() == 0 || serviceType.GetConstructor(new Type[0]) != null)
                    {
                        proxy = ProxyGenerator.CreateClassProxy(serviceType, interceptors.ToArray());
                    }
                    else
                    {
                        if (ctors.Length == 1)
                        {
                            var args = ctors.First().GetParameters().Select(p => serviceProvider.GetService(p.ParameterType)).ToArray();
                            proxy = ProxyGenerator.CreateClassProxy(serviceType, ProxyGenerationOptions.Default, args, interceptors.ToArray());
                        }
                        else
                        {
                            throw new Exception("proxy generate error");
                        }
                    }
                }
                else
                {
                    proxy = ProxyGenerator.CreateInterfaceProxyWithoutTarget(serviceType, interceptors.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("proxy generate error");
            }
            if (proxy == null)
            {
                throw new Exception("proxy generate error");
            }
            return proxy;
        }
    }
}