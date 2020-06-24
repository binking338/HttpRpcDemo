using System;

namespace HttpRpc
{
    public interface IRemoteServiceProxyGenerator
    {
        object CreateProxy(IServiceProvider serviceProvider, Type serviceType);
    }
}