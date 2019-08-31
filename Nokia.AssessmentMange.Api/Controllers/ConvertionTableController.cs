using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nokia.AssessmentMange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionTableController : ControllerBase
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="subjectId"></param>
        public void CreateConversion(string subjectId)
        { }
    }
}