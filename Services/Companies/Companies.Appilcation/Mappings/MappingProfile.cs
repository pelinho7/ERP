using AutoMapper;
using Companies.Appilcation.Features.Companies;
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
            CreateMap<Company, CompanyVm>().ReverseMap();
        }
    }
}
