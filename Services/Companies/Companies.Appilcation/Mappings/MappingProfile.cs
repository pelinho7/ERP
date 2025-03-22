using AutoMapper;
using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.CompanyUsers;
using Companies.Application.Features.Companies.Commands.ArchiveCompany;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Commands.UpdateCompany;
using Companies.Domain.Entities;

namespace Companies.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CreateCompanyCommand>().ReverseMap();
            CreateMap<Company, UpdateCompanyCommand>().ReverseMap();
            CreateMap<Company, ArchiveCompanyCommand>().ReverseMap();
            CreateMap<CompanyUser, CompanyUserVm>().ReverseMap();
            //CreateMap<Company, CompanyVm>().ReverseMap();
            CreateMap<Company, CompanyVm>()
                .ForMember(dest => dest.CompanyCurrentUser, opt => opt.MapFrom(src => src.CompanyUsers.FirstOrDefault()));
            CreateMap<CompanyVm, Company>();

            CreateMap<Company, CompanyHistory>().ReverseMap();
            CreateMap<Company, CompanyHistory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy == null ? src.CreatedBy : src.LastModifiedBy))
            .ForMember(dest => dest.CreatedModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate == null ? src.CreatedDate : src.LastModifiedDate));

            CreateMap<CompanyUser, CompanyUserHistory>().ReverseMap();
            CreateMap<CompanyUser, CompanyUserHistory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.CompanyUserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy == null ? src.CreatedBy : src.LastModifiedBy))
            .ForMember(dest => dest.CreatedModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate == null ? src.CreatedDate : src.LastModifiedDate));
        }
    }
}
