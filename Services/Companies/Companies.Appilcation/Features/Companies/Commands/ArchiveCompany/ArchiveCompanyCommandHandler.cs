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

namespace Companies.Application.Features.Companies.Commands.ArchiveCompany
{
    public class ArchiveCompanyCommandHandler : IRequestHandler<ArchiveCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ArchiveCompanyCommandHandler> _logger;

        public ArchiveCompanyCommandHandler(ICompanyRepository companyRepository
            , IMapper mapper, ILogger<ArchiveCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(ArchiveCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyToArchive = await _companyRepository.GetCompanyById(request.Id);
            if (companyToArchive == null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }
            
            _mapper.Map(request, companyToArchive, typeof(ArchiveCompanyCommand), typeof(Company));

            await _companyRepository.UpdateAsync(companyToArchive);

            _logger.LogInformation($"Company {companyToArchive.Id} is successfully archived.");

            return companyToArchive.Id;
        }
    }
}
