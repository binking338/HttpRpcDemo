using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpRpcDemo.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpRpcDemo.BizService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {

        [HttpGet]
        public string Get([FromServices]IAdd add, [FromServices]ISub sub)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            sub.Calc(1, 1);
            long a = random.Next();
            long b = random.Next();
            return $"{a} + {b} = {add.Calc(a,b)}";
        }
    }
}
