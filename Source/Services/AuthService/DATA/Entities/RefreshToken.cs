namespace AuthService.DATA.Entities;

public class RefreshToken
{
	public int Id { get; set; }
	public string Token { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime ExpiresAt { get; set; }
	public DateTime? RevokedAt { get; set; }
	public bool IsExpired { get; set; }
	public bool IsUsed { get; set; }
	public bool IsInvalidated { get; set; }
	public bool IsActive => !IsExpired && !IsUsed && !IsInvalidated;

	public string UserId { get; set; }
	public AppUser User { get; set; }



	/// <summary>
	/// Validates a refresh token and returns its validation result.
	/// </summary>
	public RefreshTokenValidationResult ValidateRefreshToken()
	{
		if (IsExpired || ExpiresAt < DateTime.UtcNow)
		{
			return RefreshTokenValidationResult.Expired;
		}

		if (RevokedAt != null)
		{
			return RefreshTokenValidationResult.Revoked;
		}

		if (IsUsed)
		{
			return RefreshTokenValidationResult.Used;
		}

		if(IsInvalidated)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		return RefreshTokenValidationResult.Valid;
	}

	/// <summary>
	/// Marks the refresh token as expired.
	/// </summary>
	public void MarkAsExpired()
	{
		IsExpired = true;
	}


	/// <summary>
	/// Sets the validation result of the refresh token to the given value.
	/// </summary>
	/// <param name="validationResult">The validation result.</param>
	public void SetTheValidation(RefreshTokenValidationResult validationResult)
	{
		switch (validationResult)
		{
			case RefreshTokenValidationResult.Used:
				IsUsed = true;
				break;
			case RefreshTokenValidationResult.Invalid:
				IsInvalidated = true;
				break;
			case RefreshTokenValidationResult.Revoked:
				RevokedAt = DateTime.UtcNow;
				break;
			case RefreshTokenValidationResult.Expired:
				MarkAsExpired();
				break;
		}
	}


	/// <summary>
	/// Updates the status of the refresh token to the given value.
	/// </summary>
	public void UpdateStatus(RefreshTokenStatus status)
	{
		switch (status)
		{
			case RefreshTokenStatus.Used:
				IsUsed = true;
				break;
			case RefreshTokenStatus.Invalidated:
				IsInvalidated = true;
				break;
			case RefreshTokenStatus.Revoked:
				RevokedAt = DateTime.UtcNow;
				break;
			case RefreshTokenStatus.Expired:
				MarkAsExpired();
				break;
		}
	}


	/// <summary>
	/// Updates the status of the refresh token to the given value.
	/// </summary>
	public void UpdateStatus(RefreshTokenValidationResult validationResult)
	{
		switch (validationResult)
		{
			case RefreshTokenValidationResult.Used:
				IsUsed = true;
				break;
			case RefreshTokenValidationResult.Invalid:
				IsInvalidated = true;
				break;
			case RefreshTokenValidationResult.Revoked:
				RevokedAt = DateTime.UtcNow;
				break;
			case RefreshTokenValidationResult.Expired:
				MarkAsExpired();
				break;
		}
	}
}