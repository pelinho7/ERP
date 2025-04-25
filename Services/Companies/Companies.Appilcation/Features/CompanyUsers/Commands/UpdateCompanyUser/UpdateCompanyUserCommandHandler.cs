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
using Companies.Appilcation.Features.CompanyUsers.Commands.UpdateCompanyUser;
using Companies.Appilcation.Features.CompanyUsers;
using Companies.Appilcation.Features.CompanyUsers.Commands;
using Companies.Application.Exceptions;

namespace Companies.Application.Features.Companies.Commands.UpdateCompanyUser
{
    public class UpdateCompanyUserCommandHandler : CompanyUserCommandHandlerBase, IRequestHandler<UpdateCompanyUserCommand, CompanyUserVm>
    {
        private readonly ILogger<UpdateCompanyUserCommandHandler> _logger;

        public UpdateCompanyUserCommandHandler(ICompanyUserRepository companyUserRepository
            , ICompanyUserHistoryRepository companyUserHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<UpdateCompanyUserCommandHandler> logger)
            :base(companyUserRepository, companyUserHistoryRepository, transactionManager, mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyUserVm> Handle(UpdateCompanyUserCommand request, CancellationToken cancellationToken)
        {
            var companyUserEntity = _mapper.Map<CompanyUser>(request);
            try
            {
                var adminPermission = await CheckUserAdminPermission(companyUserEntity.CompanyId, companyUserEntity.LastModifiedBy.Value);

                var companyUserToUpdate = await _companyUserRepository.GetCompanyUserById(request.Id);
                if (companyUserToUpdate == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                if(companyUserToUpdate.Administrator && !companyUserEntity.Administrator)
                {
                    var companyAdmins = await _companyUserRepository.GetCompanyAdmins(request.CompanyId);
                    if(!companyAdmins.Any(x=>x.Id!= companyUserToUpdate.Id))
                    {
                        throw new InvalidOperationException("Every company must have at least one administrator.");
                    }
                }

                _transactionManager.BeginTransaction();
                await _companyUserRepository.UpdateAsync(companyUserEntity);

                var companyUserHistoryEntity = _mapper.Map<CompanyUserHistory>(companyUserToUpdate);
                await _companyUserHistoryRepository.AddAsync(companyUserHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company user {companyUserToUpdate.Id} is successfully updated.");

                return _mapper.Map<CompanyUserVm>(companyUserToUpdate);
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
