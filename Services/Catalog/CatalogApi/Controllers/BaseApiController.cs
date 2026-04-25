using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
