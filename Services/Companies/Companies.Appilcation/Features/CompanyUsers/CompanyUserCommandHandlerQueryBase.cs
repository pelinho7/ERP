using AutoMapper;
using Companies.Appilcation.Contracts.Persistence;
using Companies.Application.Contracts.Persistence;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.Companies
{
    public abstract class CompanyUserCommandHandlerQueryBase
    {
        protected readonly ICompanyUserRepository _companyUserRepository;
        protected readonly IMapper _mapper;

        public CompanyUserCommandHandlerQueryBase(ICompanyUserRepository companyUserRepository
            , IMapper mapper)
        {
            _companyUserRepository = companyUserRepository ?? throw new ArgumentNullException(nameof(companyUserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
