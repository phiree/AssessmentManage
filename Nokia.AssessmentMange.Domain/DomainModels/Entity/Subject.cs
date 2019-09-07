using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目基类
    /// </summary>
    public class Subject : EntityBase
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
            this.SubjectConversions = new List<SubjectConversion>();
        }
        public IList<SubjectConversion> SubjectConversions { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(100)]
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


        public void ValidateConversions()
        {
        }
        public SubjectConversion GetSubjectConversion(Sex sex)
        {
            SubjectConversion SubjectConversion = null;
            if (SubjectConversions.Count == 0)
            {
                throw new Exceptions.ConversionNotCreated(Name);
            }
            //根据性别确定换算表
            switch (SexLimitation)
            {
                case SexLimitation.BothAndSameConversion:
                    SubjectConversion = SubjectConversions[0];
                    break;
                case SexLimitation.BothButDiffirentConversion:
                    try
                    {

                        SubjectConversion = SubjectConversions.First(x => x.Sex == sex);
                    }
                    catch
                    {
                        throw new Exceptions.ConversionNotFound(sex, this.Name);
                    }
                    break;
                case SexLimitation.FemaleOnly:
                    if (sex == Sex.Female)

                    { SubjectConversion = SubjectConversions[0]; }
                    break;
                case SexLimitation.MaleOnly:
                    if (sex == Sex.Male)

                    { SubjectConversion = SubjectConversions[0]; }
                    break;
            }
            return SubjectConversion;

        }




    }



    /// <summary>
    /// 计算类型的科目
    /// </summary>
    public class ComputedSubject : Subject
    {
        protected ComputedSubject() { }

        private ComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation,
                   bool isQualifiedConversion, string unit, string formula)
            : base(name, subjectType, sexLimitation, isQualifiedConversion, unit)
        {
            this.Formula = formula;
        }
        //
        public ComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation,
                  bool isQualifiedConversion, string unit, IDictionary<int, string> paramSubjectIds, string formula)
           : this(name, subjectType, sexLimitation, isQualifiedConversion, unit, formula)
        {
            this.ParamSubjects = paramSubjectIds.Select(x => new ParamSubject(x.Key, x.Value)).ToList();



        }
        //单元测试使用
        public ComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation,
                   bool isQualifiedConversion, string unit, IList<ParamSubject> paramSubjects, string formula)
            : this(name, subjectType, sexLimitation, isQualifiedConversion, unit, formula)
        {
            this.ParamSubjects = paramSubjects;
        }
        /// <summary>
        /// 参与计算的科目
        /// </summary>
        public IList<ParamSubject> ParamSubjects { get; protected set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; protected set; }
        public void ChangeParamSubject(IList<ParamSubject> newParams)
        {
            this.ParamSubjects.Clear();
            this.ParamSubjects = newParams;
        }

        public void UpdateComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation, bool isQualifiedConversion, string unit, string formula, IList<ParamSubject> paramSubjects)
        {
            this.Name = name;
            this.SubjectType = subjectType;
            this.SexLimitation = sexLimitation;
            this.IsQualifiedConversion = isQualifiedConversion;
            this.Unit = unit;
            this.Formula = formula;
            this.ParamSubjects = paramSubjects;
        }
    }
    /// <summary>
    /// 计算科目的 科目参数
    /// </summary>
    public class ParamSubject
    {
        protected ParamSubject() { }
        public ParamSubject(int sortOrder, string subjectId)
        {
            this.SortOrder = sortOrder; this.PSubjectId = subjectId;
        }
        /// <summary>
        /// 用于单元测试.
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="subject"></param>
        public ParamSubject(int sortOrder, Subject subject)
        {
            this.SortOrder = sortOrder; this.PSubject = subject;
        }
        public ParamSubject(int sortOrder, string pSubjectId, string pSubjectName) : this(sortOrder, pSubjectId)
        {
            this.PSubjectName = pSubjectName;
        }
        public int SortOrder { get; set; }
        public string PSubjectId { get; set; }
        public Subject PSubject { get; set; }
        public string PSubjectName { get; set; }
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
