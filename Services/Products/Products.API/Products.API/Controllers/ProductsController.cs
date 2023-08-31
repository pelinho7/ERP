using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Products.Appilcation.Features.Products;
using Products.Appilcation.Features.Products.Commands;
using Products.Application.Features.Orders.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.ArchiveProduct;
using Products.Application.Features.Products.Commands.CreateProduct;
using Products.Application.Features.Products.Commands.UpdateProduct;
using Products.Application.Features.Products.Queries.GetProductByCode;
using Products.Application.Features.Products.Queries.GetProductById;
using Products.Application.Features.Products.Queries.GetProductsList;
using Products.Domain.Entities;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [NonAction]
        private int getCompanyId()
        {
            var companyId = User.Claims.FirstOrDefault(x => x.Type == "companyId")?.Value;
            return int.Parse(companyId);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> GetOne(int id)
        {
            GetProductByIdQuery command = new GetProductByIdQuery(getCompanyId(),id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(List<ProductVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string name="",int pageNumber = 1, int pageSize=100)
        {
            GetProductsListQuery command = new GetProductsListQuery(getCompanyId(), name, pageNumber,pageSize);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost(Name = "Add")]
        [ProducesResponseType(typeof(ProductVm),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductVm>> AddProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut(Name = "Update")]
        [ProducesResponseType(typeof(ProductVm), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductVm>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch(Name = "Archive")]
        [ProducesResponseType(typeof(ProductVm), (int)StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductVm>> ArchiveProduct([FromBody] ArchiveProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        //[HttpGet("{code}", Name = "check-code-not-taken")]
        [Route("check-code-not-taken/{code}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckCodeNotTaken(string code)
        {
            GetProductByCodeQuery command = new GetProductByCodeQuery(getCompanyId(), code);
            var result = await _mediator.Send(command);
            return Ok(result == null);
        }
    }
}
