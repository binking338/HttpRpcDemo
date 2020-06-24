using System.Reflection;

namespace HttpRpc
{
    public interface IRemoteServiceInvoker
    {
        object Call(string url, MethodInfo serviceMethodInfo, object[] parameters);
    }
}