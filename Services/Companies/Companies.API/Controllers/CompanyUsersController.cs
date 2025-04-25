using Companies.Appilcation.Features.Companies;
using Companies.Appilcation.Features.CompanyUsers;
using Companies.Appilcation.Features.CompanyUsers.Commands.ArchiveCompanyUser;
using Companies.Appilcation.Features.CompanyUsers.Commands.CreateCompanyUser;
using Companies.Appilcation.Features.CompanyUsers.Commands.UpdateCompanyUser;
using Companies.Application.Features.Companies.Commands.ArchiveCompany;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Queries.GetUserCompanies;
using Companies.Application.Features.CompanyUsers.Queries.GetCompanyUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Companies.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyUsersController : Controller
    {
        private readonly IMediator _mediator;

        public CompanyUsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [NonAction]
        private int getUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId);
        }

        [Authorize]
        [HttpGet(Name = "GetCompanyUsers")]
        [ProducesResponseType(typeof(List<CompanyUserVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int companyId)
        {
            var userId = getUserId();
            GetCompanyUsersQuery command = new GetCompanyUsersQuery(companyId, userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost(Name = "AddCompanyUser")]
        [ProducesResponseType(typeof(CompanyUserVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyUserVm>> AddCompanyUser([FromBody] CreateCompanyUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut(Name = "UpdateCompanyUser")]
        [ProducesResponseType(typeof(CompanyUserVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyUserVm>> UpdateCompanyUser([FromBody] UpdateCompanyUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch(Name = "ArchiveCompanyUser")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> ArchiveCompanyUser([FromBody] ArchiveCompanyUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
