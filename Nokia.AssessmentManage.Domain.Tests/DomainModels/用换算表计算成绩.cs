using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
 
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
             var conversionTable=new ConversionTable();
            conversionTable.Init(new List<AgeRange> { new AgeRange(21,25)},new List<int> { 0,1} );
            conversionTable.SetGrade(new AgeRange(21, 25),0,169);
            conversionTable.SetGrade(new AgeRange(21, 25), 1, 170);
            var result=conversionTable.CalculateScore(true,21,168);
            Assert.Equal(0,result);
            result = conversionTable.CalculateScore(true, 22, 169);
            Assert.Equal(0, result);
            result = conversionTable.CalculateScore(true, 23, 175);
            Assert.Equal(1, result);
            result = conversionTable.CalculateScore(true, 25, 170);
            Assert.Equal(1, result);
        }
        
        [Fact]
       
        public void 合格类型_换算表数据不足()
        {
            var conversionTable = new ConversionTable();
            conversionTable.Init(new List<AgeRange> { new AgeRange(21, 25) }, new List<int> {  1 });
           
            conversionTable.SetGrade(new AgeRange(21, 25), 1, 170);
          
            Assert.Throws<QualifiedSubjectMapError>(
         () => conversionTable.CalculateScore(true, 25, 170));    
        }
       
        
        #endregion

        [Fact]
        public void 数值精确匹配()
        {
            var conversionTable = new ConversionTable();
            conversionTable.Init(new List<AgeRange> { new AgeRange(21, 25) }, new List<int> { 100,90,70 });
            conversionTable.SetGrade(new AgeRange(21, 25), 100, 13);
            conversionTable.SetGrade(new AgeRange(21, 25), 90, 14);
            conversionTable.SetGrade(new AgeRange(21, 25), 70, 15);

           
            var result = conversionTable.CalculateScore( false, 24,13);
            Assert.Equal(100, result);
              result = conversionTable.CalculateScore(false, 24, 15);
            Assert.Equal(70, result);

        }
        /// <summary>
        /// 插值算法
        /// </summary>
        [Fact]
        public void 插值算法计算()
        {
            var conversionTable = new ConversionTable();
            conversionTable.Init(new List<AgeRange> { new AgeRange(21, 25) }, new List<int> { 100, 90, 80 });
            conversionTable.SetGrade(new AgeRange(21, 25), 100, 13);
            conversionTable.SetGrade(new AgeRange(21, 25), 90, 14);
            conversionTable.SetGrade(new AgeRange(21, 25), 80, 15);


            
            var result = conversionTable.CalculateScore(false, 24, 13.5);
            Assert.True((99.9350649350649-result)<0.000001);
            

        }
         

    }
}
