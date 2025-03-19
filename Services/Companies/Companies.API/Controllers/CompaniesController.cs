using Companies.Appilcation.Features.Companies;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Commands.UpdateCompany;
using Companies.Application.Features.Companies.Queries.GetCompanyById;
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

        //[Authorize]
        [HttpGet(Name = "GetCompanies")]
        [ProducesResponseType(typeof(List<CompanyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            //GetProductsListQuery command = new GetProductsListQuery(getCompanyId(), name, page, pageSize);
            //var result = await _mediator.Send(command);
            //return Ok(result);
            return null;
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

        //[Authorize]
        //[HttpPatch(Name = "Archive")]
        //[ProducesResponseType((int)StatusCodes.Status200OK)]
        //public async Task<ActionResult<ProductVm>> ArchiveProducts([FromBody] IEnumerable<ArchiveProductCommand> commands)
        //{
        //    foreach (var command in commands)
        //    {
        //        var result = await _mediator.Send(command);
        //    }
        //    return Ok();
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("check-code-not-taken/{code}")]
        //[ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<bool>> CheckCodeNotTaken(string code,int? id)
        //{
        //    CheckProductCodeNotTakenQuery command = new CheckProductCodeNotTakenQuery(getCompanyId(), code,id);
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("all")]
        //[ProducesResponseType(typeof(List<ProductVm>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<List<ProductVm>>> GetAll()
        //{
        //    //GetAllProductsQuery command = new GetAllProductsQuery(getCompanyId());
        //    //var result = await _mediator.Send(command);
        //    //return Ok(result);
        //    return Ok(new List<ProductVm>());
        //}
    }
}
