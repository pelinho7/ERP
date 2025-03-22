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

namespace Companies.Appilcation.Features.Companies.Commands
{
    public abstract class CompanyCommandHandlerBase:CompanyCommandHandlerQueryBase
    {
        protected readonly ICompanyHistoryRepository _companyHistoryRepository;
        protected readonly ITransactionManager _transactionManager;

        public CompanyCommandHandlerBase(ICompanyRepository companyRepository
            , ICompanyHistoryRepository companyHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper) : base(companyRepository, mapper)
        {
            _companyHistoryRepository = companyHistoryRepository ?? throw new ArgumentNullException(nameof(companyHistoryRepository));
            _transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
        }
    }
}
