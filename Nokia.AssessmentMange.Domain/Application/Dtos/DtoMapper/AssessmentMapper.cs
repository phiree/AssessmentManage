using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Domain.Application.Dtos.DtoMapper
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
            Assessment assessment=new Assessment(model.DepartmentId,model.Name,model.Annual);
           return assessment;

        }
    }
}
