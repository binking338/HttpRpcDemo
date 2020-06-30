using System;

namespace HttpRpc
{
    /// <summary>
    /// 远程服务代理生成器
    /// </summary>
    public interface IRemoteServiceProxyGenerator
    {
        /// <summary>
        /// 生成代理
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="serviceType">服务接口类型</param>
        /// <returns></returns>
        object CreateProxy(IServiceProvider serviceProvider, Type serviceType);
    }
}