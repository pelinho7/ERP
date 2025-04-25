using Companies.Appilcation.Features.Companies;
using Companies.Application.Features.Companies.Commands.ArchiveCompany;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Commands.UpdateCompany;
using Companies.Application.Features.Companies.Queries.GetCompanyById;
using Companies.Application.Features.Companies.Queries.GetUserCompanies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Net;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Companies.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
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
        [HttpGet("{id}", Name = "GetCompany")]
        [ProducesResponseType(typeof(CompanyVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> GetOne(int id)
        {
            var userId = getUserId();
            GetCompanyByIdQuery command = new GetCompanyByIdQuery(id, userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet(Name = "GetCompanies")]
        [ProducesResponseType(typeof(List<CompanyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var userId = getUserId();
            GetUserCompaniesQuery command = new GetUserCompaniesQuery(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost(Name = "Add")]
        [ProducesResponseType(typeof(CompanyVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> AddCompany([FromBody] CreateCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut(Name = "Update")]
        [ProducesResponseType(typeof(CompanyVm), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<CompanyVm>> UpdateProduct([FromBody] UpdateCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch(Name = "Archive")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<CompanyVm>> ArchiveProducts([FromBody] ArchiveCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
