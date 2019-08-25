using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nokia.AssessmentMange.Domain;
using Nokia.AssessmentMange.Domain.DomainModels;
namespace Nokia.AssessmentMange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        IConfiguration config;
        IOptionsMonitor<SampleOption> option;
        IOptionsMonitor<SampleOption2> option2;
        IOptions<DbConnectionOption> dbOption;
    ILogger<SamplesController> logger;
        IRepository repo;
        IExcelExporter excelExporter;
        public SamplesController(IConfiguration config, 
            IOptionsMonitor<SampleOption> option,
          IOptionsMonitor<SampleOption2> option2,
            IOptions<DbConnectionOption> dbOption,
            ILogger<SamplesController> logger,
              IRepository repo, IExcelExporter excelExporter
            )
        {
            this.config = config;
            this.option = option;
            this.option2 = option2;
            this.dbOption = dbOption;
            this.logger = logger;
            this.repo = repo;
            this.excelExporter = excelExporter;
        }

        [HttpGet("ValueFromConfig")]
        public ActionResult<string> ValueFromConfig()
        {
            try
            {
                return config["SampleConfigKey"];
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet("ValueFromOption")]
        public ActionResult<string> ValueFromOption()
        {

            return $"option.CurrentValue.Option1:{ option.CurrentValue.Option1}"
                   +$",option2.CurrentValue.Option1:{ option2.CurrentValue.Option1}";
        }
       
        [HttpGet("ConnectionStringFromOption")]
        public ActionResult<string> ConnectionStringFromOption()
        {
            return dbOption.Value.SampleConn;
        }
        [HttpGet("WriteLoggerToFile")]
        public ActionResult<string> WriteLoggerToFile()
        {
            logger.LogInformation("this is debug");
            return "debug has been logged";
        }
        [HttpGet("AccessRepository")]
        public ActionResult<string> AccessRepository()
        {
            repo.WriteLogSample();
           
            return "repository log has been logged";
        }
        [HttpGet("GetSampleList")]
        public ActionResult<dynamic> GetSampleList()
        {
            return repo.GetSampleList(dbOption.Value.SampleConn);
        }

        /// <summary>
        /// 新增的导出模版
        /// </summary>
        /// <returns></returns>
        [HttpGet("ExcelList")]
        public dynamic ExcelList()
        {
            try
            {
                DataTable tblDatas = new DataTable("Datas");
                DataColumn dc = null;
                dc = tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
                dc = tblDatas.Columns.Add("Product", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("Version", Type.GetType("System.String"));
                dc = tblDatas.Columns.Add("Description", Type.GetType("System.String"));
                DataRow newRow;
                newRow = tblDatas.NewRow();
                newRow["ID"] = 1;
                newRow["Product"] = "大话西游";
                newRow["Version"] = "2.0";
                newRow["Description"] = "我很喜欢";
                tblDatas.Rows.Add(newRow);

                System.Data.DataSet ds = new DataSet();
                tblDatas.TableName = "投诉清单";
                ds.Tables.Add(tblDatas);
                var ms = excelExporter.ExportStream(ds);
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}