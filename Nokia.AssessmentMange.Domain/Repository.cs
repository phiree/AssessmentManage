using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Nokia.AssessmentMange.Domain
{
    public class Repository:IRepository
    {
          ILogger<Repository> logger;
        DbConnectionOption  dbconnections;
        public Repository(ILogger<Repository> logger,IOptions<DbConnectionOption> dbconnectionOption)
        {
            this.logger = logger;
            this.dbconnections = dbconnectionOption.Value;


        }
        private IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public void WriteLogSample()
        {
            logger.LogInformation("write log in repository sample");
        }
        public dynamic GetSampleList(string connectionstring)
        {
            string sql = "select top 10 * from BsCompany";
            DomainLogger.Log(logger, sql);

            var result = GetConnection(dbconnections.SampleConn).Query(sql);

            return result;
           
        }
        
    }
}
