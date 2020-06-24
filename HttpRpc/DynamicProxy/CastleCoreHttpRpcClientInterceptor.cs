using System;
using Castle.DynamicProxy;

namespace HttpRpc
{
    public class CastleCoreHttpRpcClientInterceptor : IInterceptor
    {
        private IHttpClientInvoker httpClientInvoker;

        public CastleCoreHttpRpcClientInterceptor(IHttpClientInvoker httpClientInvoker)
        {
            this.httpClientInvoker = httpClientInvoker;
        }

        public void Intercept(IInvocation invocation)
        {
            var returnValue = httpClientInvoker.Call(invocation.Method, invocation.Arguments);
            invocation.ReturnValue = returnValue;
        }
    }

}
