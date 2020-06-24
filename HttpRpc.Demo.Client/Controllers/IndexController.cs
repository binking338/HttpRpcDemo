using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpRpc.Demo.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpRpc.Demo.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {

        [HttpGet("add")]
        public string Add([FromServices]IAdd add, int a, int b)
        {
            return $"{a} + {b} = {add.Calc(a,b)}";
        }

        [HttpGet("sub")]
        public string Sub([FromServices]ISub sub, int a, int b)
        {
            return $"{a} - {b} = {sub.Calc(a, b)}";
        }
    }
}
