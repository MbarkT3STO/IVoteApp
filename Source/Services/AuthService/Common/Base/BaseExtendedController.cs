namespace AuthService.Common.Base;

/// <summary>
/// Base controller class that provides extended functionality for controllers.
/// </summary>
public abstract class BaseExtendedController(IMediator mediator, IMapper mapper) : ControllerBase
{
	protected readonly IMediator _mediator = mediator;
	protected readonly IMapper _mapper = mapper;
}