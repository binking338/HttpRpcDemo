using System;
using HttpRpc.Demo.Interface;
using Microsoft.Extensions.Logging;

namespace HttpRpc.Demo.Server.Services
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
            logger.LogInformation($"入参：a = {a}; b = {b};");
            var r = a - b;
            logger.LogInformation($"计算结果：a - b = {r};");
            return r;
        }
    }
}
