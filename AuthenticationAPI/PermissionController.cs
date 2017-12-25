using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ocelot.JWTAuthorizePolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationAPI
{
    public class PermissionController : Controller
    {
        PermissionRequirement _requirement;
        public PermissionController(PermissionRequirement requirement)
        {
            _requirement = requirement;
        }

        [AllowAnonymous]
        [HttpPost("/authapi/login")]
        public IActionResult Login([FromBody]LoginModel login)
        {
            var isValidate = (login.UserName == "ryan" && login.Password == "12345678") || (login.UserName == "paul" && login.Password == "111111");
            var role = login.UserName == "ryan" ? "admin" : "system";
            if (!isValidate)
            {
                return new JsonResult(new
                {
                    Status = false,
                    Message = "认证失败"
                });
            }
            else
            {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name,login.UserName),
                    new Claim(ClaimTypes.Role,role),
                    new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                var token = JwtToken.BuildJwtToken(claims, _requirement);
                return new JsonResult(token);
            }
        }

        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
