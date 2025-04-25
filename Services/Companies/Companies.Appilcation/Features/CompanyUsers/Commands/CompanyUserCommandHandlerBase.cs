using AutoMapper;
using Companies.Appilcation.Contracts.Persistence;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers.Commands
{
    public class CompanyUserCommandHandlerBase: CompanyUserCommandHandlerQueryBase
    {
        protected readonly ICompanyUserHistoryRepository _companyUserHistoryRepository;
        protected readonly ITransactionManager _transactionManager;

        public CompanyUserCommandHandlerBase(ICompanyUserRepository companyUserRepository
            , ICompanyUserHistoryRepository companyUserHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper) : base(companyUserRepository, mapper)
        {
            _companyUserHistoryRepository = companyUserHistoryRepository ?? throw new ArgumentNullException(nameof(companyUserHistoryRepository));
            _transactionManager = transactionManager ?? throw new ArgumentNullException(nameof(transactionManager));
        }

        public async Task<bool> CheckUserAdminPermission(int comapnyId, int userId)
        {
            var companyUsers = await _companyUserRepository.GetCompanyUsers(comapnyId);
            if (!companyUsers.Any(x=>x.Administrator && x.UserId == userId))
            {
                throw new System.Security.Authentication.AuthenticationException("No administrator rights.");
            }
            return true;
        }
    }
}
