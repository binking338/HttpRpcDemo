using System;
using HttpRpc.Demo.Interface;
using Microsoft.Extensions.Logging;

namespace HttpRpc.Demo.CalcService.Services
{
    public class SubImpl : ISub
    {
        public ILogger<SubImpl> logger;
        public SubImpl(ILogger<SubImpl> logger)
        {
            this.logger = logger;
        }

        public long Calc(long a, long b)
        {
            logger.LogInformation($"a = {a}; b = {b};");
            var r = a - b;
            logger.LogInformation($"a - b = {r};");
            return r;
        }
    }
}
