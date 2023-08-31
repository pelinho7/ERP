using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Contractors.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContractorsController : ControllerBase
    {
        [Authorize]
        //[Authorize("email")]
        [HttpGet("{id}", Name = "GetBasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetContractor(int id)
        {
            var a = HttpContext.Request;
            //var basket = await _repository.GetBasket(userName);
            return Ok();
        }
    }
}
