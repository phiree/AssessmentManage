using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Api.Models;
using Nokia.AssessmentMange.Api.Controllers.Authentication;
namespace Nokia.AssessmentMange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticateService authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }
        [AllowAnonymous]
        [HttpPost("RequestToken")]
        public ActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }
            string token;
            if(authenticateService.IsAuthenticated(request,out token))
            { 
                return Ok(token);
                }
            return BadRequest("Invalid Request");

        }
        [AllowAnonymous]
        [HttpGet("RequestToken2")]
        public ActionResult RequestToken2()
        {
            return Ok("bb");

        }
        [AllowAnonymous]
        [HttpGet("RequestToken3")]
        public ActionResult RequestToken3()
        {
            return Ok("cc");

        }
    }
}