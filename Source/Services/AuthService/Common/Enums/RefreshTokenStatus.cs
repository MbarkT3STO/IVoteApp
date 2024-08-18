namespace AuthService.Common.Enums;

/// <summary>
/// Represents the status of the refresh token.
/// </summary>
public enum RefreshTokenStatus
{
	Expired,
	Revoked,
	Invalidated,
	Active
}
