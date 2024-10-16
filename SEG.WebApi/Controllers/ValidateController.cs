using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEG.WebApi.Filters;

namespace SEG.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : Controller
    {
        [HttpGet("Token")]
        [ValidateTokenRequest]
        public IActionResult Token()
        {
            return Ok();
        }
    }
}
