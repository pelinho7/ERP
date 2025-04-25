using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Application.Exceptions;
using Companies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Companies.Appilcation.Features.Companies.Commands;
using Companies.Appilcation.Contracts.Persistence;

namespace Companies.Application.Features.Companies.Commands.ArchiveCompany
{
    public class ArchiveCompanyCommandHandler : CompanyCommandHandlerBase,IRequestHandler<ArchiveCompanyCommand, int>
    {
        private readonly ILogger<ArchiveCompanyCommandHandler> _logger;

        public ArchiveCompanyCommandHandler(ICompanyRepository companyRepository
            , ICompanyHistoryRepository companyHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<ArchiveCompanyCommandHandler> logger)
            : base(companyRepository, companyHistoryRepository, transactionManager, mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ArchiveCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToArchive = await _companyRepository.GetCompanyById(request.Id);
            if (companyToArchive == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }
            var userPermissions = companyToArchive.CompanyUsers.Where(x => x.UserId == request.LastModifiedBy).FirstOrDefault();
            if (userPermissions == null || !userPermissions.Administrator)
            {
                throw new System.Security.Authentication.AuthenticationException("No administrator rights.");
            }
            _mapper.Map(request, companyToArchive, typeof(ArchiveCompanyCommand), typeof(Company));

            try
            {
                _transactionManager.BeginTransaction();

                companyToArchive.Archived = true;
                await _companyRepository.UpdateAsync(companyToArchive);

                var companyHistoryEntity = _mapper.Map<CompanyHistory>(companyToArchive);
                await _companyHistoryRepository.AddAsync(companyHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company {companyToArchive.Id} is successfully archived.");

                return companyToArchive.Id;
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
