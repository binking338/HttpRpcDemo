using System;
using System.Reflection;

namespace HttpRpc
{
    /// <summary>
    /// 泛化调用器
    /// </summary>
    public interface IServiceInvoker
    {
        object Call(IServiceProvider serviceProvider, MethodInfo methodInfo, object[] parameters);
    }
}