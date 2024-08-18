namespace AuthService.Extensions;

public static class RefreshTokenValidationResultExtensions
{
	/// <summary>
	/// Converts <see cref="RefreshTokenValidationResult"/> to <see cref="RefreshTokenStatus"/>
	// /// </summary>
	// /// <param name="result">The validation result to convert</param>
	// public static RefreshTokenStatus ToStatus(this RefreshTokenValidationResult result)
	// {
	// 	return result switch
	// 	{
	// 		RefreshTokenValidationResult.Expired => RefreshTokenStatus.Expired,
	// 		RefreshTokenValidationResult.Invalid => RefreshTokenStatus.Invalidated,
	// 		RefreshTokenValidationResult.Revoked => RefreshTokenStatus.Revoked,
	// 		_ => RefreshTokenStatus.Used,
	// 	};
	// }
}
