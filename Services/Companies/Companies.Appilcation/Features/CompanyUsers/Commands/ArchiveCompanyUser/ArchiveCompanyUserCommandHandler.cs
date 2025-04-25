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

namespace Companies.Appilcation.Features.CompanyUsers.Commands.ArchiveCompanyUser
{
    public class ArchiveCompanyUserCommandHandler : CompanyUserCommandHandlerBase, IRequestHandler<ArchiveCompanyUserCommand, int>
    {
        private readonly ILogger<ArchiveCompanyUserCommandHandler> _logger;

        public ArchiveCompanyUserCommandHandler(ICompanyUserRepository companyUserRepository
            , ICompanyUserHistoryRepository companyUserHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<ArchiveCompanyUserCommandHandler> logger)
            :base(companyUserRepository, companyUserHistoryRepository, transactionManager, mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ArchiveCompanyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var companyUserToArchive = await _companyUserRepository.GetCompanyUserById(request.Id);
                if (companyUserToArchive == null)
                {
                    throw new NotFoundException(nameof(Company), request.Id);
                }

                var adminPermission = await CheckUserAdminPermission(companyUserToArchive.CompanyId, request.LastModifiedBy.Value);

                if (companyUserToArchive.Administrator)
                {
                    var companyAdmins = await _companyUserRepository.GetCompanyAdmins(companyUserToArchive.CompanyId);
                    if (!companyAdmins.Any(x => x.Id != companyUserToArchive.Id))
                    {
                        throw new InvalidOperationException("Every company must have at least one administrator.");
                    }
                }

                companyUserToArchive.Archived = true;
                _transactionManager.BeginTransaction();
                await _companyUserRepository.UpdateAsync(companyUserToArchive);

                var companyUserHistoryEntity = _mapper.Map<CompanyUserHistory>(companyUserToArchive);
                await _companyUserHistoryRepository.AddAsync(companyUserHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company user {companyUserToArchive.Id} is successfully archived.");

                return companyUserToArchive.Id;
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
