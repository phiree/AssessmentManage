using AutoMapper;
using Nokia.AssessmentMange.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nokia.AssessmentMange.Api.Models.DtoMapper
{
    /// <summary>
    /// VO转换类
    /// </summary>
    public class AssessmentManageVOProfile : Profile
    {
        public AssessmentManageVOProfile()
        {
            CreateMap<PersonVO, Person>()
                .ForMember(des => des.MilitaryRank, opt => opt.MapFrom(src => src.Rank))
                .ForMember(des => des.RealName, opt => opt.MapFrom(src => src.Name));

            CreateMap<PersonChangeVO, Person>()
            .ForMember(des => des.MilitaryRank, opt => opt.MapFrom(src => src.Rank))
            .ForMember(des => des.RealName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
