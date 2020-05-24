using System;
using HttpRpcDemo.Interface;

namespace HttpRpcDemo.CalcService
{
    public class AddImpl : IAdd
    {
        public AddImpl()
        {
        }

        public long Calc(long a, long b)
        {
            return a + b;
        }
    }
}
