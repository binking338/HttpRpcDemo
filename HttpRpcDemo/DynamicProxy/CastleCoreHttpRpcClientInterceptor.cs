using System;
using Castle.DynamicProxy;

namespace HttpRpcDemo
{
    public class CastleCoreHttpRpcClientInterceptor : IInterceptor
    {
        private HttpClientInvoker httpClientInvoker;

        public CastleCoreHttpRpcClientInterceptor(HttpClientInvoker httpClientInvoker)
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
