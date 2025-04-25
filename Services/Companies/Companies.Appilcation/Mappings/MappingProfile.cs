using AutoMapper;
using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.CompanyUsers;
using Companies.Appilcation.Features.CompanyUsers.Commands.CreateCompanyUser;
using Companies.Appilcation.Features.CompanyUsers.Commands.UpdateCompanyUser;
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
            CreateMap<Company, CompanyBasicVm>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.VatId, opt => opt.MapFrom(src => src.VatId))
            //.ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

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

            CreateMap<CompanyUser, CreateCompanyUserCommand>().ReverseMap();
            CreateMap<CompanyUser, UpdateCompanyUserCommand>().ReverseMap();
        }
    }
}
