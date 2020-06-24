using System;
using System.Reflection;

namespace HttpRpc
{
    public interface IServiceInvoker
    {
        object Call(IServiceProvider serviceProvider, MethodInfo methodInfo, object[] parameters);
    }
}