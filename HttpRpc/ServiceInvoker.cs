using System;
using System.Reflection;

namespace HttpRpc
{
    public class ServiceInvoker : IServiceInvoker
    {
        public object Call(
            IServiceProvider serviceProvider,
            MethodInfo methodInfo,
            object[] parameters)
        {
            var targetType = methodInfo.DeclaringType;
            var serviceInstance = serviceProvider.GetService(targetType);
            var result = methodInfo.Invoke(serviceInstance, parameters);
            return result;
        }
    }
}
