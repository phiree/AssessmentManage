using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP=Dapper.Contrib;
 
namespace Nokia.AssessmentMange.Domain.Infrastructure.Repository.Dapper
{
    public class SqlBuilderForGeneric<T>
    {
        public SqlBuilderForGeneric()
        {
            GetAttributeValue();
            }
        public string BuildQuery(DynamicParameters dp)
        {
          
            string sql = $"select * from {tableName} ";
            string where = " where 1=1 ";
            foreach (var paramName in dp.ParameterNames)
            {
                var value = dp.Get<dynamic>(paramName);
                Type t = value.GetType();
                string wrap = string.Empty;
                if (t.ToString() == typeof(String).ToString())
                {
                    wrap = "'";
                }

                where += $" and {paramName}={wrap}{dp.Get<dynamic>(paramName)}{wrap} ";
            }
            return sql+where;
        }
        string tableName, keyIdName;
        private void GetAttributeValue()
        {
            //  dynamic tableattr =
            var attributes = typeof(T).GetCustomAttributes(false);
            if (attributes != null)
            {
              var tableAttr=(DP.Extensions.TableAttribute) attributes.SingleOrDefault(attr =>attr.GetType().Name == "TableAttribute");
                tableName=tableAttr.Name;
            }
            tableName = string.IsNullOrEmpty(tableName) ? nameof(T) : tableName;


        }

    }
}
