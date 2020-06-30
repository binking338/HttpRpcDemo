using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace HttpRpc.DynamicProxy
{
    public class CastleCoreHttpRpcClientInterceptor : IInterceptor
    {
        private IRemoteServiceInvoker httpClientInvoker;
        private ILogger<CastleCoreHttpRpcClientInterceptor> logger;

        public CastleCoreHttpRpcClientInterceptor(IRemoteServiceInvoker httpClientInvoker, ILogger<CastleCoreHttpRpcClientInterceptor> logger)
        {
            this.httpClientInvoker = httpClientInvoker;
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            logger.LogInformation("发起远程调用...");
            var returnValue = httpClientInvoker.Call(UrlMap.GetValueOrDefault(invocation.Method.DeclaringType), invocation.Method, invocation.Arguments);
            invocation.ReturnValue = returnValue;
            logger.LogInformation("远程调用结束...");
        }

        internal static Dictionary<Type, string> UrlMap = new Dictionary<Type, string>();
    }

}
