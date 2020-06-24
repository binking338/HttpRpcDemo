using System;
using Castle.Core.Logging;
using HttpRpc.Demo.Interface;
using Microsoft.Extensions.Logging;

namespace HttpRpc.Demo.CalcService
{
    public class AddImpl : IAdd
    {
        public ILogger<AddImpl> logger;
        public AddImpl(ILogger<AddImpl> logger)
        {
            this.logger = logger;
        }

        public long Calc(long a, long b)
        {
            logger.LogInformation($"a = {a}; b = {b};");
            var r = a + b;
            logger.LogInformation($"a + b = {r};");
            return r;
        }
    }
}
