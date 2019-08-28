using System;
using System.Collections.Generic;
                                                                        using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nokia.AssessmentMange.Domain.DomainModels
{
    /// <summary>
    /// 科目基类
    /// </summary>
    public class Subject : Entity.EntityBase
    {
        protected Subject() { }
        public Subject(string name  ,SubjectType subjectType, SexLimitation sexLimitation,
            bool isQualifiedConversion,string unit)
        { 
            this.Name=name;
            this.SubjectType=subjectType;
            this.SexLimitation=sexLimitation;
            this.IsQualifiedConversion=isQualifiedConversion;
            this.Unit=unit;
            }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// 分类
        /// </summary>
        public SubjectType SubjectType { get; protected set; }
        public  SexLimitation SexLimitation { get; protected set; }
        /// <summary>
        /// 得分换算表是否使用 合格/不合格  
        /// </summary>
        [Column("IsQualifiedConversion",TypeName="bit")]
        public bool IsQualifiedConversion { get;protected set;}
        public string Unit { get; protected set; }

        public void Update(string name,SubjectType subjectType,SexLimitation sexLimitation,bool isQualifiedConversion,string unit)
        {
            this.Name = name;
            this.SubjectType = subjectType;
            this.SexLimitation = sexLimitation;
            this.IsQualifiedConversion = isQualifiedConversion;
            this.Unit = unit;
        }
        
    }
  
    
    
    /// <summary>
    /// 计算类型的科目
    /// </summary>
    public class ComputedSubject : Subject
    {
        public ComputedSubject(string name, SubjectType subjectType, SexLimitation sexLimitation,
                   bool isQualifiedConversion, string unit, IDictionary<int, Subject> paramSubjects,string formula)
            :base(name,subjectType,sexLimitation,isQualifiedConversion,unit)
        {
            this.ParamSubjects = paramSubjects;
       
            this.Formula=formula;
            
        }
        /// <summary>
        /// 参与计算的科目
        /// </summary>
        public IDictionary<int,Subject> ParamSubjects { get; protected set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; protected set; }
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
    public enum  SexLimitation
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
