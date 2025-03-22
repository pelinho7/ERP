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
    public abstract class CompanyCommandHandlerQueryBase
    {
        protected readonly ICompanyRepository _companyRepository;
        protected readonly IMapper _mapper;

        public CompanyCommandHandlerQueryBase(ICompanyRepository companyRepository
            , IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
