using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Contractors.Appilcation.Features.Contractors;
using Contractors.Appilcation.Features.Contractors.Commands;
using Contractors.Domain.Entities;
using System.Net;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Contractors.Application.Features.Contractors.Queries.CheckContractorCodeNotTaken;
using Contractors.Application.Features.Contractors.Commands.ArchiveContractor;
using Contractors.Application.Features.Contractors.Commands.UpdateContractor;
using Contractors.Application.Features.Contractors.Commands.CreateContractor;
using Contractors.Application.Features.Contractors.Queries.GetContractorsList;
using Contractors.Application.Features.Contractors.Queries.GetContractorById;

namespace Contractors.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractorsController : Controller
    {
        private readonly IMediator _mediator;

        public ContractorsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [NonAction]
        private int getCompanyId()
        {
            var companyId = User.Claims.FirstOrDefault(x => x.Type == "companyId")?.Value;
            return int.Parse(companyId);
        }

        [NonAction]
        private int getUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return int.Parse(userId);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetContractor")]
        [ProducesResponseType(typeof(ContractorVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContractorVm>> GetOne(int id)
        {
            GetContractorByIdQuery command = new GetContractorByIdQuery(getCompanyId(),id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet(Name = "GetContractors")]
        [ProducesResponseType(typeof(List<ContractorVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string filter="",int page = 1, int pageSize=10)
        {
            GetContractorsListQuery command = new GetContractorsListQuery(getCompanyId(), filter, page,pageSize);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost(Name = "Add")]
        [ProducesResponseType(typeof(ContractorVm),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ContractorVm>> AddContractor([FromBody] CreateContractorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut(Name = "Update")]
        [ProducesResponseType(typeof(ContractorVm), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractorVm>> UpdateContractor([FromBody] UpdateContractorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch(Name = "Archive")]
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractorVm>> ArchiveContractors([FromBody] IEnumerable<ArchiveContractorCommand> commands)
        {
            foreach (var command in commands)
            {
                var result = await _mediator.Send(command);
            }
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("check-code-not-taken/{code}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckCodeNotTaken(string code,int? id)
        {
            CheckContractorCodeNotTakenQuery command = new CheckContractorCodeNotTakenQuery(getCompanyId(), code,id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
