using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nokia.AssessmentMange.Api.Controllers.Authentication;
using Nokia.AssessmentMange.Domain.DomainModels;

namespace Nokia.AssessmentMange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        private IAuthenticateService _authenticateService;

        public BaseController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }


        internal User GetUserInfo()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                return _authenticateService.ParseToken(Request.Headers["Authorization"].ToString());
            }
            return null;
        }
    }
}