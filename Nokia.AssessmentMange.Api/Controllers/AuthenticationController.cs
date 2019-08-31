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
    /// <summary>
    /// 用户认证
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthenticateService authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        
    }
}