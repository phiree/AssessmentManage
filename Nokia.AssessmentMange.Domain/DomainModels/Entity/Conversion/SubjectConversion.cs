using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目的成绩换算对照表
    /// </summary>
    public class SubjectConversion
    {
        public SubjectConversion(Sex sex,Subject subject,IList<AgeConversion> ageConversions)
        { 
             this.Sex=sex;
            this.Subject=subject;
            this.AgeConversions=ageConversions;
            }
        public Sex Sex { get; set; }
        public Subject Subject { get; protected set; }
        public IList<AgeConversion> AgeConversions { get; protected set; }
        public string ComputeScore(int age,  double grade)
        {
            var _ageConversions = AgeConversions.Where(x => x.AgeRange.InRange(age));
            if (_ageConversions.Count() != 1)
            {
                throw new Exceptions.AgeNotSuitable(age, Subject.Name, Sex);
            }
            var scoreGrades = _ageConversions.First().ScoreGrades;
            double maxGradeinConversion = scoreGrades.Max(x => x.Grade);
            double minGradeinConversion = scoreGrades.Min(x => x.Grade);
            var conversionWithMax = scoreGrades.Single(x => x.Grade == maxGradeinConversion);
            var conversionWithMin = scoreGrades.Single(x => x.Grade == minGradeinConversion);
            if (Subject.IsQualifiedConversion)
            {
                //必须配置两条
                if(scoreGrades.Count!=2)
                { 
                    throw new Exceptions.QualifiedSubjectMapError(Subject.Name,scoreGrades.Count);
                    }
                //1:合格 0:不合格
                //判断 成绩数值越大,得分越高.
                bool biggerBetter = conversionWithMax.Score > conversionWithMin.Score
                                    && maxGradeinConversion > minGradeinConversion;
                bool isQualified = biggerBetter ? grade >= maxGradeinConversion : grade <= minGradeinConversion;

                return isQualified ? "合格" : "不合格";
            }

            //数值成绩

            if (grade > maxGradeinConversion)
            {
                throw new Exceptions.GradeBeyondMaximum(grade, maxGradeinConversion);
            }
            if (grade < minGradeinConversion)
            {
                throw new Exceptions.GradeBeyondMinimum(grade, minGradeinConversion);
            }

            var nearestConversions = GetNearest(scoreGrades, grade);
            if (nearestConversions.Count == 1)
            {
                return nearestConversions[0].Score.ToString();
            }
            else if (nearestConversions.Count == 2)
            {
                var first = nearestConversions[0];
                var second = nearestConversions[1];
                var score = new InterpolationScore(first.Score, second.Score, first.Grade, second.Score, grade).GetValue();
                return score.ToString();
            }
            else
            {
                throw new Exception($"计算分值错误.成绩对照表没有找到对应分数或者临近分数.科目:{Subject.Name},性别:{Sex.ToString()},人员成绩:{grade}");
            }

        }
        /// <summary>
        /// 获取最近的对照项,如果精确匹配,返回一个,否则,返回两个.
        /// </summary>
        /// <param name="scoreConversions"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public IList<ScoreGrade> GetNearest(IList<ScoreGrade> scoreGrade, double grade)
        {
            var nearest = scoreGrade.Where(x => x.Grade == grade);
            if (nearest.Count() == 1)
            {
                return nearest.ToList();
            }
            nearest = scoreGrade.OrderBy(x => Math.Abs(x.Grade - grade));
            if (nearest.Count() < 2)
            {
                throw new Exceptions.ScoreGradeMapIncomplete(Subject.Name, grade);
            }
           return  nearest.Take(2).ToArray();

        }

    }
}
