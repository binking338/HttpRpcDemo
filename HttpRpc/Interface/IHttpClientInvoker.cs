using System.Reflection;

namespace HttpRpc
{
    public interface IHttpClientInvoker
    {
        object Call(MethodInfo serviceMethodInfo, object[] parameters);
    }
}