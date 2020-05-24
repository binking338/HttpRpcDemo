using System;

namespace HttpRpcDemo
{
    public class DIInvoker
    {
        public object Call(
            IServiceProvider serviceProvider,
            string typeName,
            string methodName,
            object[] parameters)
        {
            var targetType = Type.GetType(typeName);
            var serviceInstance = serviceProvider.GetService(targetType);
            var methodInfo = targetType.GetMethod(methodName);
            var result = methodInfo.Invoke(serviceInstance, parameters);
            return result;
        }
    }
}
