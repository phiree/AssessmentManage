using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Nokia.AssessmentMange.Domain.DomainModels;
using Nokia.AssessmentMange.Domain.Application.Dtos;
namespace Nokia.AssessmentMange.Domain.Infrastructure.DtoMapper
{
    public class AssessmentManageProfile:Profile
    {
        public AssessmentManageProfile()
        {
            CreateMap<SubjectModel, Subject>();
             
        }
    }
}
