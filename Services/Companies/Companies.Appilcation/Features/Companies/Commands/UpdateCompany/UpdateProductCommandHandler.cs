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

namespace Companies.Application.Features.Companies.Commands.UpdateCompany
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyVm>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(ICompanyRepository companyRepository
            , IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CompanyVm> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToUpdate = await _companyRepository.GetCompanyById(request.Id);
            if (companyToUpdate == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }
            //check if current user is admin for company 
            _mapper.Map(request, companyToUpdate, typeof(UpdateCompanyCommand), typeof(Company));

            await _companyRepository.UpdateAsync(companyToUpdate);

            _logger.LogInformation($"Company {companyToUpdate.Id} is successfully updated.");

            return _mapper.Map<CompanyVm>(companyToUpdate);
        }
    }
}
