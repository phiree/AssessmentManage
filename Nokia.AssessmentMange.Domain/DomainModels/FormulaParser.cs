using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 将公式解析为运行时执行的代码
    /// </summary>
    public class FormulaParser
    {
        public string Parse(string formula, IDictionary<int, double?> subjectGrades)
        {
            string p=@"\$(\d+)";
            var matches=Regex.Matches(formula,p);
            string resultExpression=formula;
            var reg=new Regex(p);
           string code= reg.Replace(formula,m=>subjectGrades[Convert.ToInt32(m.Groups[1].Value)].ToString()+"m");
            
            return code;

        }
    }
}
