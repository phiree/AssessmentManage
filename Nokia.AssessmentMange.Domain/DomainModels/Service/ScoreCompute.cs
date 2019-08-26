using Nokia.AssessmentMange.Domain.DomainModels.Entity;
using Nokia.AssessmentMange.Domain.DomainModels.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels.Service
{
    /// <summary>
    /// 得分计算
    /// 计算参数: 年龄,性别, 科目, 科目成绩换算表,科目成绩.
    /// </summary>
    public class ScoreCompute 
    {
        
       ISubjectRepository subjectRepository;
         
        public ScoreCompute()
        { }
        public string Compute(string subjectId,Person person,double grade)
        { 
            IList<SubjectConversion> scoreConversions= subjectRepository.GetSubjectConversions(subjectId);

            Subject subject=subjectRepository.Get(subjectId);
            double maxGradeinConversion = scoreConversions.Max(x => x.Grade);
            double minGradeinConversion = scoreConversions.Min(x => x.Grade);
            if (subject.IsQualifiedConversion)
            { 
                //判断 成绩越高越好,还是越低越好.
                bool biggerBetter=maxGradeinConversion>minGradeinConversion;
                if(biggerBetter && grade>=maxGradeinConversion) return "合格";
                else if(biggerBetter&& grade<=minGradeinConversion) return "不合格";
                }

            //成绩是否超出对照表
           
             if (grade> maxGradeinConversion)
                { 
                throw new Exceptions.GradeBeyondMaximum(grade,maxGradeinConversion);
                }
          
            if (grade < minGradeinConversion)
            {
                throw new Exceptions.GradeBeyondMinimum(grade, minGradeinConversion);
            }
            

            //获取最近的取值
            var nearestConversion= GetNearest(scoreConversions,grade);
            throw new NotImplementedException();
            }

        public ScoreConversion GetNearest(IList<ScoreConversion> scoreConversions,double grade)
        {
           var maybeNearest=  scoreConversions. Aggregate((x, y) => Math.Abs(x.Score- grade) < Math.Abs(y.Score - grade) ? x : y);
            
            
        }
    }
}
