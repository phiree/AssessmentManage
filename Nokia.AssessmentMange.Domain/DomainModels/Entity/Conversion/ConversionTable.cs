using System;
using System.Collections.Generic;
using System.Linq;
namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 成绩对照表, 抽象为一个表格
    /// 主要逻辑为 增加行,增加列,更新单元格.
    /// </summary>
    public class ConversionTable
    {
        public ConversionTable()
        {
            Grades = new List<ConversionCell>();
        }
        //columns
        public IEnumerable<AgeRange> AgeRanges
        {
            get { return Grades.Select(x => x.AgeRange).Distinct(); }
        }
        //rows
        public IEnumerable<double> Scores
        {
            get
            {
                return Grades.Select(x => x.Score).Distinct();
            }
        }
        // cells 
        public IList<ConversionCell> Grades { get; protected set; }
        public void Init(IList<AgeRange> ageRanges, IList<double> scores)
        {

            foreach (AgeRange ar in ageRanges)
            {
                foreach (double score in scores)
                {
                    var gradeCellValue = new ConversionCell(ar, score, null);
                    Grades.Add(gradeCellValue);
                }
            }
        }

        public void AddAgeRange(AgeRange ageRange)
        {
            if (Grades.Count == 0)
            {
                throw new Exceptions.ConversionTableNotInitialized();
            }
            if (AgeRanges.Where(x => x.IsCoincide(ageRange)).Count() > 0)
            {
                throw new Exceptions.AgeRangeCoincide(ageRange);

            }
            foreach (var score in Scores.ToArray())
            {
                Grades.Add(new ConversionCell(ageRange, score, null));
            }
        }
        public void AddScore(double score)
        {
            if (Grades.Count == 0)
            {
                throw new Exceptions.ConversionTableNotInitialized();
            }
            if (Scores.Contains(score))
            {
                throw new Exceptions.ScoreAlreadyExisted(score);

            }

            foreach (var ageRange in AgeRanges.ToArray())
            {
                Grades.Add(new ConversionCell(ageRange, score, null));
            }
        }
        public void SetGrade(AgeRange ageRange, double score, double? grade)
        {
            ConversionCell existed = null;
            try
            {
                existed = Grades.First(x => x.AgeRange.Equals(ageRange) && x.Score == score);

            }
            catch (System.InvalidOperationException)
            {
                throw new Exceptions.ConversionCellNotFound(ageRange, score);
            }
            Grades.Remove(existed);
            Grades.Add(new ConversionCell(ageRange, score, new Grade(grade)));

        }
        public void RemoveAge(AgeRange ageRange)
        {
            var existedGrades = Grades.Where(x => x.AgeRange == ageRange && Scores.Contains(x.Score));
            foreach (var grade in existedGrades)
            {
                Grades.Remove(grade);
            }
        }
        public void RemoveScore(double score)
        {
            var existedGrades = Grades.Where(x => x.Score == score && AgeRanges.Contains(x.AgeRange));
            foreach (var grade in existedGrades)
            {
                Grades.Remove(grade);
            }
        }
        /// <summary>
        /// 成绩换算
        /// </summary>
        /// <param name="isQualifiedConversion">是否是 合格/不合格 类型的换算. 0 不合格,1 合格</param>
        /// <param name="age"></param>
        /// <param name="grade">可能为null,什么都不填</param>
        /// <returns></returns>
        public double CalculateScore(bool isQualifiedConversion, int age, double grade)
        {
            //年龄对应的换算项
            var ageConversions = Grades.Where(x => x.AgeRange.ContainsValue(age));

            if (ageConversions.Count() ==0)
            {
                throw new Exceptions.AgeConversionNotFound(age);
            }
            //换算项的范围
            Grade maxGrade = ageConversions.Max(x => x.Grade);
            Grade minGrade = ageConversions.Min(x => x.Grade);
            var gradeRange = new Range<Grade>(minGrade, maxGrade);


            var conversionWithMaxGrade = ageConversions.Single(x => x.Grade == maxGrade);
            var conversionWithMinGrade = ageConversions.Single(x => x.Grade == minGrade);
            //合格 不合格 的换算
            if (isQualifiedConversion)
            {
                //必须配置两条
                if (ageConversions.Count() != 2)
                {
                    throw new Exceptions.QualifiedSubjectMapError(ageConversions.Count());
                }
                //1:合格 0:不合格
                //判断 成绩数值越大,得分越高.
                bool biggerBetter = conversionWithMaxGrade.Score > conversionWithMinGrade.Score
                                    && maxGrade > minGrade;
                bool isQualified = biggerBetter ? grade >= maxGrade.GradeValue : grade <= minGrade.GradeValue;

                return isQualified ? 1 : 0;
            }

            //数值成绩
            //成绩超出范围
            if (grade > maxGrade.GradeValue)
            {
                throw new Exceptions.GradeBeyondMaximum(grade, maxGrade.GradeValue);
            }
            if (grade < minGrade.GradeValue)
            {
                throw new Exceptions.GradeBeyondMinimum(grade, minGrade.GradeValue);
            }

            var nearestConversions = GetNearest(grade);
            if (nearestConversions.Count == 1)
            {
                return nearestConversions[0].Score ;
            }

            var first = nearestConversions[0];
            var second = nearestConversions[1];
            var score = new InterpolationScore(first.Score, second.Score, first.Grade.GradeValue, second.Score, grade).GetValue();
            return score ;

        }
        /// <summary>
        /// 获取最近的对照项,如果精确匹配,返回一个,否则,返回两个.
        /// </summary>
        /// <param name="scoreConversions"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public IList<ConversionCell> GetNearest(double grade)
        {
            var nearest = Grades.Where(x => x.Grade.GradeValue == grade);
            if (nearest.Count() == 1)
            {
                return nearest.ToList();
            }
            nearest = Grades.OrderBy(x => Math.Abs(x.Grade.GradeValue - grade));
            if (nearest.Count() < 2)
            {
                throw new Exceptions.ScoreGradeMapIncomplete(grade);
            }

            return nearest.Take(2).ToArray();
        }
    }

    public class ConversionCell
    {
        protected ConversionCell() { }
        public ConversionCell(AgeRange ageRange, double score, Grade grade)
        {
            this.AgeRange = ageRange;
            this.Score = score;
            this.Grade = grade;
        }
        public AgeRange AgeRange { get; protected set; }//column
        public double Score { get; protected set; }  //row
        public Grade Grade { get; protected set; }//cell
    }
    /// <summary>
    /// 年龄范围
    /// </summary>
    public class AgeRange : Range<int>
    {

        protected AgeRange() { }
        public AgeRange(int floorAge, int cellingAge)
        {
            if (floorAge > cellingAge) { throw new Exceptions.AgeRangeError(cellingAge, floorAge); }
            this.CellingAge = cellingAge;
            this.FloorAge = floorAge;
        }
        public int CellingAge { get { return Maximum; } set { Maximum = value; } }
        public int FloorAge { get { return Minimum; } set { Minimum = value; } }

    }

    public class ScoreGrade
    {
        protected ScoreGrade() { }
        public ScoreGrade(double grade, double score)
        {
            this.Grade = grade;
            this.Score = score;
        }
        public double Grade { get; protected set; }
        public double Score { get; protected set; }
    }
}
