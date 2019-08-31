using System;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目成绩
    /// </summary>
    public class SubjectGrade
    {
        protected SubjectGrade() { }
        public SubjectGrade(Subject subject,double? grade)
        { 
            this.Subject=subject;
            this.Grade=grade;
            }
        public string SubjectId { get;protected set;}
        public Subject Subject { get;protected set;}
        public double? Grade { get;   set; }
        public double Score {get;set; }
        public void SetScore(double score)
        { 
            Score=score;
            }
        public string DisplayScore {
            get {
                if (Subject.IsQualifiedConversion) { 
                    return Score==0?"不合格":"合格";
                    }
                return Score.ToString("{0:2}");
                }
            }

        
    }


}
