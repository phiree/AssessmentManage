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
            var subjectConversion=new  SubjectConversion( Sex.Male,
                new  Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion,true,"秒")
                ,new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                        new ScoreGrade(169,0),
                        })
                    });

          var result=  subjectConversion.ComputeScore(24,168);
            Assert.Equal("不合格",result);
            result = subjectConversion.ComputeScore(24, 169);
            Assert.Equal("不合格", result);
        }
        [Fact]
        public void 合格类型_合格()
        {
            var subjectConversion = new SubjectConversion(Sex.Male,
                new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒")
                , new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                        new ScoreGrade(169,0),
                        })
                    });

            var result = subjectConversion.ComputeScore(24, 170);
            Assert.Equal("合格", result);
              result = subjectConversion.ComputeScore(24, 178);
            Assert.Equal("合格", result);
        }
        [Fact]
       
        public void 合格类型_换算表数据不足()
        {
            var subjectConversion = new SubjectConversion(Sex.Male,
                new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒")
                , new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                  
                        })
                    });
     Assert.Throws<QualifiedSubjectMapError>(
         ()=> subjectConversion.ComputeScore(24, 168));    
        }
        [Fact]

        public void 合格类型_换算表数据太多()
        {
            var subjectConversion = new SubjectConversion(Sex.Male,
                new Subject("身高", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, true, "秒")
                , new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(170,1),
                         new ScoreGrade(157,0),
                          new ScoreGrade(178,1),
                        })
                    });
            Assert.Throws<QualifiedSubjectMapError>(
                () => subjectConversion.ComputeScore(24,  168));
        }
         
        #endregion

        [Fact]
        public void 数值精确匹配()
        {
            var subjectConversion = new SubjectConversion(Sex.Male,
                new Subject("百米跑", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒")
                , new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(13,100),
                        new ScoreGrade(14,90),
                         new ScoreGrade(15,70),
                        })
                    });

            var result = subjectConversion.ComputeScore(24,13);
            Assert.Equal("100", result);
              result = subjectConversion.ComputeScore(21, 15);
            Assert.Equal("70", result);

        }
        /// <summary>
        /// 插值算法
        /// </summary>
        [Fact]
        public void 插值算法计算()
        {
            var subjectConversion = new SubjectConversion(Sex.Male,
                new Subject("百米跑", SubjectType.PhysicalFitness, SexLimitation.BothButDiffirentConversion, false, "秒")
                , new List<AgeConversion> {
                    new AgeConversion(new AgeRange(21,25)
                    ,new List<ScoreGrade>{
                        new ScoreGrade(13,100),
                        new ScoreGrade(14,90),
                         new ScoreGrade(15,70),
                        })
                    });
            var result = subjectConversion.ComputeScore(24, 13.5);
            Assert.Equal("99.9350649350649", result);
            

        }
    }
}
