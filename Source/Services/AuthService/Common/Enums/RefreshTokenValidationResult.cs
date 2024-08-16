namespace AuthService.Common.Enums;

public enum RefreshTokenValidationResult
{
	Expired,
	Valid,
	Revoked,
	Used,
	Invalid
}
