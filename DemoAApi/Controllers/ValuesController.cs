using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DemoAApi.Controllers
{
    [Authorize("Permission")]
    [Route("demoaapi/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "DemoA", "Request" };
        }

        [AllowAnonymous]
        [HttpGet("/demoaapi/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new { Status = false, Message ="demoaapi你无权访问"});
        }
    }
}
