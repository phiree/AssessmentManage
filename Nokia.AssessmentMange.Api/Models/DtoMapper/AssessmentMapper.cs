using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models.DtoMapper
{
    /// <summary>
    /// assessment model 和 实体转换. 量少, 暂时没必要使用automapper
    /// </summary>
    public class AssessmentMapper : IAssessmentMapper
    {
        public AssessmentMapper()
        { }
        public Assessment ToEntity(AssessmentModel model)
        {
            bool isNew = model is AssessmentCreateModel;
            // if new create 
            // else get from repository
            throw new NotImplementedException();

        }
    }
}
