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
using Products.Application.Features.Products.Queries.CheckProductCodeNotTaken;
using Products.Application.Features.Products.Queries.GetProductByCode;
using Products.Application.Features.Products.Queries.GetProductById;
using Products.Application.Features.Products.Queries.GetProductsList;
using Products.Domain.Entities;
using System.Net;
using System.Security.Claims;
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

        [NonAction]
        private int getUserId()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return int.Parse(userId);
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
        public async Task<ActionResult> Get(string name="",int page = 1, int pageSize=10)
        {
            GetProductsListQuery command = new GetProductsListQuery(getCompanyId(), name, page,pageSize);
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
        [ProducesResponseType((int)StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductVm>> ArchiveProducts([FromBody] IEnumerable<ArchiveProductCommand> commands)
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
            CheckProductCodeNotTakenQuery command = new CheckProductCodeNotTakenQuery(getCompanyId(), code,id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
