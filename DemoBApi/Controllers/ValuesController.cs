using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DemoBApi.Controllers
{
    [Authorize("Permission")]
    [Route("demobapi/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "DemoB", "Request" };
        }

        [AllowAnonymous]
        [HttpGet("/demobapi/denied")]
        public IActionResult Denied()
        {
            return new JsonResult(new { Status = false, Message = "demobapi你无权访问" });
        }
    }
}
