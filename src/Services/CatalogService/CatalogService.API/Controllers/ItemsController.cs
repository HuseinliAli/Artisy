using CatalogService.Application.Features.Items.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllItemsQuery request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }
    }
}
