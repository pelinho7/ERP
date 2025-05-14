using MediatR;
using Companies.Appilcation.Features.Companies;
using System;
using System.Collections.Generic;
using Companies.Appilcation.Features.CompanyUsers;

namespace Companies.Application.Features.CompanyUsers.Queries.CheckCompanyUserModulePermission
{
    public class CheckCompanyUserModulePermissionQuery : IRequest<bool>
    {
        public CheckCompanyUserModulePermissionQuery() { }
        public CheckCompanyUserModulePermissionQuery(int companyId, int userId, int modulePermission)
        {
            this.CompanyId = companyId;
            this.UserId = userId;
            this.ModulePermission = modulePermission;
        }

        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int ModulePermission { get; set; }
    }
}
