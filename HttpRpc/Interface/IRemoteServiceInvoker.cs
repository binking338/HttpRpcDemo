using System.Reflection;

namespace HttpRpc
{
    /// <summary>
    /// 远程服务调用器
    /// </summary>
    public interface IRemoteServiceInvoker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="serviceMethodInfo">服务方法反射信息</param>
        /// <param name="parameters">调用参数</param>
        /// <returns></returns>
        object Call(string url, MethodInfo serviceMethodInfo, object[] parameters);
    }
}