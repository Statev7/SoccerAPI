namespace SoccerAPI.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public abstract class BaseAPIController : ControllerBase
	{
	}
}
