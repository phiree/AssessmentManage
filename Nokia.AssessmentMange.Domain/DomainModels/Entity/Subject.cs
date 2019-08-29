using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目基类
    /// </summary>
    public class Subject : Entity.EntityBase
    {
        protected Subject() { }
        public Subject(string name, SubjectType subjectType, SexLimitation sexLimitation,
            bool isQualifiedConversion, string unit)
        {
            this.Name = name;
            this.SubjectType = subjectType;
            this.SexLimitation = sexLimitation;
            this.IsQualifiedConversion = isQualifiedConversion;
            this.Unit = unit;
        }
        public IList<SubjectConversion> SubjectConversions { get;  set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// 分类
        /// </summary>
        public SubjectType SubjectType { get; protected set; }
        public SexLimitation SexLimitation { get; protected set; }
        /// <summary>
        /// 得分换算表是否使用 合格/不合格  
        /// </summary>
        [Column("IsQualifiedConversion", TypeName = "bit")]
        public bool IsQualifiedConversion { get; protected set; }
        public string Unit { get; protected set; }

        public void Update(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit)
        {
            this.Name = name;
            this.SubjectType = subjectType;
            this.SexLimitation = sexLimitation;
            this.IsQualifiedConversion = isQualifiedConversion;
            this.Unit = unit;
        }
        

      
        public string ComputeScore(Sex sex, int age, double grade)
        {
            SubjectConversion SubjectConversion =null;
            try { 
              SubjectConversion=SubjectConversions.First(x=>x.Sex==sex);
            }
            catch   { 
            throw new Exceptions.ConversionNotFound(sex,age,this.Name); }
            var _ageConversions = SubjectConversion. AgeConversions.Where(x => x.AgeRange.InRange(age));
            if (_ageConversions.Count() != 1)
            {
                throw new Exceptions.AgeNotSuitable(age, this.Name, SubjectConversion.Sex);
            }
            var scoreGrades = _ageConversions.First().ScoreGrades;
            double maxGradeinConversion = scoreGrades.Max(x => x.Grade);
            double minGradeinConversion = scoreGrades.Min(x => x.Grade);
            var conversionWithMax = scoreGrades.Single(x => x.Grade == maxGradeinConversion);
            var conversionWithMin = scoreGrades.Single(x => x.Grade == minGradeinConversion);
            if (this.IsQualifiedConversion)
            {
                //必须配置两条
                if (scoreGrades.Count != 2)
                {
                    throw new Exceptions.QualifiedSubjectMapError( Name, scoreGrades.Count);
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
                throw new Exception($"计算分值错误.成绩对照表没有找到对应分数或者临近分数.科目:{ Name},性别:{SubjectConversion. Sex.ToString()},人员成绩:{grade}");
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
                throw new Exceptions.ScoreGradeMapIncomplete(Name, grade);
            }
            return nearest.Take(2).ToArray();

        }

    }



    /// <summary>
    /// 计算类型的科目
    /// </summary>
    public class ComputedSubject : Subject
    {
        public ComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation,
                   bool isQualifiedConversion, string unit, IDictionary<int, Subject> paramSubjects, string formula)
            : base(name, subjectType, sexLimitation, isQualifiedConversion, unit)
        {
            this.ParamSubjects = paramSubjects;

            this.Formula = formula;

        }
        /// <summary>
        /// 参与计算的科目
        /// </summary>
        public IDictionary<int, Subject> ParamSubjects { get; protected set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; protected set; }
        public void ChangeParamSubject(IDictionary<int, Subject> newParams)
        {
            this.ParamSubjects.Clear();
            this.ParamSubjects = newParams;

        }
    }


    public enum SubjectType
    {
        /// <summary>
        /// 体能
        /// </summary>
        PhysicalFitness = 1,
        /// <summary>
        /// 智能
        /// </summary>
        Intelligent = 2,
        /// <summary>
        /// 技能
        /// </summary>
        Skill = 3
    }
    public enum SexLimitation
    {
        MaleOnly = 1,
        FemaleOnly = 2,
        /// <summary>
        /// 得分换算表不一样
        /// </summary>
        BothButDiffirentConversion = 3,
        /// <summary>
        /// 得分换算表一样
        /// </summary>
        BothAndSameConversion = 4
    }
}
