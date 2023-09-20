using AutoMapper;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Domain.Entities;

namespace Contractors.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contractor, CreateContractorCommand>().ReverseMap();
            CreateMap<Contractor, UpdateContractorCommand>().ReverseMap();
            CreateMap<Contractor, ArchiveContractorCommand>().ReverseMap();
            CreateMap<Contractor, ContractorHistory>().ReverseMap();
            CreateMap<Contractor, ContractorHistory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.ContractorId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CreatedModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy == null ? src.CreatedBy : src.LastModifiedBy))
            .ForMember(dest => dest.CreatedModifiedDate, opt => opt.MapFrom(src => src.LastModifiedDate == null ? src.CreatedDate : src.LastModifiedDate));
            CreateMap<Contractor, ContractorVm>().ReverseMap();
        }
    }
}
