using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using   Nokia.AssessmentMange.Domain.DomainModels.Entity;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.DomainModels.Exceptions;
namespace Nokia.AssessmentManage.Domain.Tests.DomainModels
{
   
   public class 根据换算表计算得分Tests
    {

        #region 合格.不合格 类型科目
        /// <summary>
        /// 合格/不合格类型科目 精确匹配
        /// </summary>
        [Fact]
        public void 合格类型科_不合格()
        { 
            var subject= new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒");

            var subjectConversion=new  SubjectConversion( Sex.Male
                
                ,new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                        new ScoreGrade(169,0),
                        })
                    });
            subject.SubjectConversions=new List<SubjectConversion>{ subjectConversion };
          var result= subject.ComputeScore(Sex.Male, 24,168);
            Assert.Equal("不合格",result);
            result = subject.ComputeScore(Sex.Male, 24, 169);
            Assert.Equal("不合格", result);
        }
        [Fact]
        public void 合格类型_合格()
        {
         var subject=   new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒");  
            var subjectConversion = new SubjectConversion(Sex.Male,
                new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                        new ScoreGrade(169,0),
                        })
                    });
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            var result = subject.ComputeScore(Sex.Male, 24, 170);
            Assert.Equal("合格", result);
              result = subject.ComputeScore(Sex.Male, 24, 178);
            Assert.Equal("合格", result);
        }
        [Fact]
       
        public void 合格类型_换算表数据不足()
        {
            var subject = new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒");
              var subjectConversion = new SubjectConversion(Sex.Male,
                new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                  
                        })
                    });
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            Assert.Throws<QualifiedSubjectMapError>(
         ()=> subject.ComputeScore(Sex.Male, 24, 168));    
        }
       
        [Fact]

        public void 合格类型_换算表数据太多()
        {
            var subject = new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒")
               ;
              var subjectConversion = new SubjectConversion(Sex.Male,
               new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                         new ScoreGrade(157,0),
                          new ScoreGrade(178,1),
                        })
                    });
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            Assert.Throws<QualifiedSubjectMapError>(
                () => subject.ComputeScore(Sex.Male, 24,  168));
        }
         
        #endregion

        [Fact]
        public void 数值精确匹配()
        {
            var subject = new Subject("百米跑", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒"); 
              var subjectConversion = new SubjectConversion(Sex.Male,
                new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(13,100),
                        new ScoreGrade(14,90),
                         new ScoreGrade(15,70),
                        })
                    });
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            var result = subject.ComputeScore(Sex.Male, 24,13);
            Assert.Equal("100", result);
              result = subject.ComputeScore(Sex.Male, 21, 15);
            Assert.Equal("70", result);

        }
        /// <summary>
        /// 插值算法
        /// </summary>
        [Fact]
        public void 插值算法计算()
        {
            var subject = new Subject("百米跑", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒");  
              var subjectConversion = new SubjectConversion(Sex.Male,
               new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(13,100),
                        new ScoreGrade(14,90),
                         new ScoreGrade(15,70),
                        })
                    }); 
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            var result = subject.ComputeScore(Sex.Male, 24, 13.5);
            Assert.Equal("99.9350649350649", result);
            

        }
        [Fact]
        public void 换算表不存在_缺少对应性别的()
        {
            var subject = new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒");
            var subjectConversion = new SubjectConversion(Sex.Male,
              new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),

                        })
                  });
            subject.SubjectConversions = new List<SubjectConversion> { subjectConversion };
            Assert.Throws<ConversionNotFound>(
         () => subject.ComputeScore(Sex.Female, 24, 168));
        }

    }
}
