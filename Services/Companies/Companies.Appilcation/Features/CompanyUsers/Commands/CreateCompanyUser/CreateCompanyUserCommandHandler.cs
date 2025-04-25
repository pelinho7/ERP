using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Transactions;
using Companies.Appilcation.Contracts.Persistence;
using Companies.Appilcation.Features.CompanyUsers.Commands.CreateCompanyUser;
using Companies.Appilcation.Features.CompanyUsers;
using Companies.Appilcation.Features.CompanyUsers.Commands;

namespace Companies.Application.Features.Companies.Commands.CreateCompanyUser
{
    public class CreateCompanyUserCommandHandler : CompanyUserCommandHandlerBase, IRequestHandler<CreateCompanyUserCommand, CompanyUserVm>
    {
        private readonly ILogger<CreateCompanyUserCommandHandler> _logger;

        public CreateCompanyUserCommandHandler(ICompanyUserRepository companyUserRepository
            , ICompanyUserHistoryRepository companyUserHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<CreateCompanyUserCommandHandler> logger)
            :base(companyUserRepository, companyUserHistoryRepository, transactionManager, mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyUserVm> Handle(CreateCompanyUserCommand request, CancellationToken cancellationToken)
        {
            var companyUserEntity = _mapper.Map<CompanyUser>(request);
            try
            {
                var adminPermission = await CheckUserAdminPermission(companyUserEntity.CompanyId, companyUserEntity.CreatedBy);
                _transactionManager.BeginTransaction();
                var newCompanyUser = await _companyUserRepository.AddAsync(companyUserEntity);

                var companyUserHistoryEntity = _mapper.Map<CompanyUserHistory>(newCompanyUser);
                await _companyUserHistoryRepository.AddAsync(companyUserHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company user {newCompanyUser.Id} is successfully created.");

                return _mapper.Map<CompanyUserVm>(newCompanyUser);
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
