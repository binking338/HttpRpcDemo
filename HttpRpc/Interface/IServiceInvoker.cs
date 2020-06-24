using System;

namespace HttpRpc
{
    public interface IServiceInvoker
    {
        object Call(IServiceProvider serviceProvider, string typeName, string methodName, object[] parameters);
    }
}