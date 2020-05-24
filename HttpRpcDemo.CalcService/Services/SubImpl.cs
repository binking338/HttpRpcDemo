using System;
using HttpRpcDemo.Interface;

namespace HttpRpcDemo.CalcService.Services
{
    public class SubImpl : ISub
    {
        public SubImpl()
        {
        }

        public long Calc(long a, long b)
        {
            return a - b;
        }
    }
}
