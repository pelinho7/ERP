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
using Companies.Application.Features.Companies.Commands.CreateCompany;
using System.Transactions;

namespace Companies.Application.Features.Companies.Commands.UpdateCompany
{
    public class UpdateProductCommandHandler : CompanyCommandHandlerBase,IRequestHandler<UpdateCompanyCommand, CompanyVm>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(ICompanyRepository companyRepository
            , ICompanyHistoryRepository companyHistoryRepository
            , ITransactionManager transactionManager
            , IMapper mapper
            , ILogger<UpdateProductCommandHandler> logger)
            : base(companyRepository, companyHistoryRepository, transactionManager, mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyVm> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToUpdate = await _companyRepository.GetCompanyById(request.Id);
            if (companyToUpdate == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }
            var userPermissions = companyToUpdate.CompanyUsers.Where(x => x.UserId == request.LastModifiedBy).FirstOrDefault();
            if (userPermissions == null || !userPermissions.Administrator)
            {
                throw new System.Security.Authentication.AuthenticationException("No administrator rights.");
            }
            //check if current user is admin for company 
            _mapper.Map(request, companyToUpdate, typeof(UpdateCompanyCommand), typeof(Company));

            try
            {
                _transactionManager.BeginTransaction();

                await _companyRepository.UpdateAsync(companyToUpdate);

                var companyHistoryEntity = _mapper.Map<CompanyHistory>(companyToUpdate);
                await _companyHistoryRepository.AddAsync(companyHistoryEntity);

                _transactionManager.CommitTransaction();

                _logger.LogInformation($"Company {companyToUpdate.Id} is successfully updated.");

                return _mapper.Map<CompanyVm>(companyToUpdate);
            }
            catch (Exception ex)
            {
                _transactionManager.RollbackTransaction();
                throw;
            }
        }
    }
}
