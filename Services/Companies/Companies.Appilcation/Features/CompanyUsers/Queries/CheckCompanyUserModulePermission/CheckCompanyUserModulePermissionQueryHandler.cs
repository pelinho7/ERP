using AutoMapper;
using CacheService;
using Companies.Appilcation.Common;
using Companies.Appilcation.Features.Companies;
using Companies.Application.Contracts.Persistence;
using Companies.Application.Exceptions;
using Companies.Application.Features.CompanyUsers.Queries.CheckCompanyUserModulePermission;
using Companies.Domain.Entities;
using Dictionaries.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Appilcation.Features.CompanyUsers.Queries.CheckCompanyUserModulePermission
{
    public class CheckCompanyUserModulePermissionQueryHandler : CompanyUserCommandHandlerQueryBase, IRequestHandler<CheckCompanyUserModulePermissionQuery, bool>
    {
        private readonly ICacheService _cacheService;
        public CheckCompanyUserModulePermissionQueryHandler(ICompanyUserRepository companyUserRepository
        , IMapper mapper,ICacheService cacheService)
        : base(companyUserRepository, mapper) 
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public void a()
        {

        }

        public async Task<bool> Handle(CheckCompanyUserModulePermissionQuery request, CancellationToken cancellationToken)
        {
            string key = CacheKeyBuilder.Build(typeof(CompanyUser), null, request.UserId, request.CompanyId);
            var companyUser = await _cacheService.GetCachedResponseAsync<CompanyUser>(key);
            
            if(companyUser == null)
            {
                companyUser = await _companyUserRepository.GetCompanyUserByUserIdCompanyId(request.UserId, request.CompanyId);
                await _cacheService.UpsertCachedResponseAsync(key, companyUser);
                if (companyUser == null)
                {
                    throw new NotFoundException("Company user", $"UserId={request.UserId}; CompanyId={request.CompanyId}");
                }
            }

            if (request.ModulePermission == (int)ModulePermissionEnum.ContractorsModuleWrite)
                return companyUser.ContractorsModuleWrite;
            else if (request.ModulePermission == (int)ModulePermissionEnum.ContractorsModuleRead)
                return companyUser.ContractorsModuleRead;
            else
            {
                throw new NotImplementedException($"Verification of module permission {(ModulePermissionEnum)request.ModulePermission} not implemented.");
            }
        }
    }
}
