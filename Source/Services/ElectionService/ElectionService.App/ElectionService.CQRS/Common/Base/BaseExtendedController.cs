using System.Security.Claims;

namespace ElectionService.CQRS.Common.Base;

/// <summary>
/// Represents a base class for extended controllers.
/// </summary>
public class BaseExtendedController : ControllerBase
{
	protected readonly IMediator _mediator;

	public BaseExtendedController(IMediator mediator)
	{
		_mediator = mediator;
	}


	/// <summary>
	/// Get user id from claims
	/// </summary>
	protected string GetUserId()
	{
		return User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
	}
}
