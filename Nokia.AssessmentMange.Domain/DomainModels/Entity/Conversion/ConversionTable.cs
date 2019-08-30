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
            Grades=new List<ConversionCell>();
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
        public IList<ConversionCell> Grades { get; protected set;}
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
            if(Grades.Count==0)
            { 
                throw new Exceptions.ConversionTableNotInitialized();
                }
            if( AgeRanges.Where(x=>x.IsCoincide(ageRange)).Count()>0 )
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
            ConversionCell existed=null;
            try { 
              existed = Grades.First(x => x.AgeRange.Equals( ageRange) && x.Score == score);

            }
            catch(System.InvalidOperationException) { 
                throw new Exceptions.ConversionCellNotFound(ageRange,score);
                }
            existed.Grade = grade;
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
    }

    public class ConversionCell
    {
        public ConversionCell(AgeRange ageRange, double score, double? grade)
        {
            this.AgeRange = ageRange;
            this.Score = score;
            this.Grade = grade;
        }
        public AgeRange AgeRange;//column
        public double Score;  //row
        public double? Grade;//cell
    }
}
