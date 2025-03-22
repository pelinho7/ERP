using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Transactions;
using Companies.Appilcation.Contracts.Persistence;
using Companies.Appilcation.Features.Companies.Commands;

namespace Companies.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : CompanyCommandHandlerBase, IRequestHandler<CreateCompanyCommand, CompanyVm>
    {
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly ICompanyUserHistoryRepository _companyUserHistoryRepository;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository
            , ICompanyHistoryRepository companyHistoryRepository
            , ICompanyUserRepository companyUserRepository
            , ICompanyUserHistoryRepository companyUserHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<CreateCompanyCommandHandler> logger)
            :base(companyRepository, companyHistoryRepository, transactionManager, mapper)
        {
            _companyUserRepository = companyUserRepository ?? throw new ArgumentNullException(nameof(companyUserRepository));
            _companyUserHistoryRepository = companyUserHistoryRepository ?? throw new ArgumentNullException(nameof(companyUserHistoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyVm> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = _mapper.Map<Company>(request);
            try
            {
                _transactionManager.BeginTransaction();
                var newCompany = await _companyRepository.AddAsync(companyEntity);

                var companyHistoryEntity = _mapper.Map<CompanyHistory>(newCompany);
                await _companyHistoryRepository.AddAsync(companyHistoryEntity);

                //set creating user as a company admin and set authorization for all modules
                CompanyUser companyUserEntity = new CompanyUser();
                companyUserEntity.CompanyId = newCompany.Id;
                companyUserEntity.UserId = newCompany.CreatedBy;
                companyUserEntity.CreatedBy = newCompany.CreatedBy;
                companyUserEntity.CreatedDate = newCompany.CreatedDate;
                companyUserEntity.Administrator = true;
                var moduleProps = companyUserEntity.GetType().GetProperties().Where(x => x.Name.Contains("Module")).ToList();
                moduleProps.ForEach(p =>
                {
                    p.SetValue(companyUserEntity, true);
                });

                await _companyUserRepository.AddAsync(companyUserEntity);

                var companyUserHistoryEntity = _mapper.Map<CompanyUserHistory>(companyUserEntity);
                await _companyUserHistoryRepository.AddAsync(companyUserHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company {newCompany.Id} is successfully created.");

                return _mapper.Map<CompanyVm>(newCompany);
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
