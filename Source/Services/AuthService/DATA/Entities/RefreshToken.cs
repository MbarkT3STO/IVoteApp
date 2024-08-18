namespace AuthService.DATA.Entities;

public class RefreshToken
{
	public int Id { get; set; }
	public string Token { get; set; }
	public DateTime CreatedAt { get; set; }

	public bool IsExpired { get; set; }
	public DateTime ExpiresAt { get; set; }
	public bool IsRevoked { get; set; }
	public DateTime? RevokedAt { get; set; }
	public bool IsUsed { get; set; }
	public DateTime? LastUsedAt { get; set; }
	public bool IsInvalidated { get; set; }
	public DateTime? InvalidatedAt { get; set; }

	public bool IsActive => !IsExpired && !IsUsed && !IsInvalidated;

	public string UserId { get; set; }
	public AppUser User { get; set; }



	/// <summary>
	/// Validates a refresh token and returns its validation result.
	/// </summary>
	public RefreshTokenValidationResult ValidateRefreshToken()
	{
		if ( IsExpired || ExpiresAt < DateTime.UtcNow)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		if ( IsRevoked || RevokedAt != null)
		{
			return RefreshTokenValidationResult.Invalid;
		}

		if( IsInvalidated || InvalidatedAt != null)
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
	/// Marks the refresh token as revoked.
	/// </summary>
	public void MarkAsRevoked()
	{
		IsRevoked = true;
		RevokedAt = DateTime.UtcNow;
	}

	/// <summary>
	/// Marks the refresh token as invalid.
	/// </summary>
	public void MarkAsInvalid()
	{
		IsInvalidated = true;
		InvalidatedAt = DateTime.UtcNow;
	}

	/// <summary>
	/// Marks the refresh token as used.
	/// </summary>
	public void MarkAsUsed()
	{
		IsUsed = true;
		LastUsedAt = DateTime.UtcNow;
	}

	/// <summary>
	/// Checks if the refresh token is expired and not marked as expired yet.
	/// </summary>
	public bool IsExpiredAndNotMarkedAsExpiredYet()
	{
		return ExpiresAt < DateTime.UtcNow && !IsExpired;
	}


	/// <summary>
	/// Gets the status of the refresh token.
	/// </summary>
	public RefreshTokenStatus GetStatus()
	{
		if (IsExpired || ExpiresAt < DateTime.UtcNow)
		{
			return RefreshTokenStatus.Expired;
		}

		if (IsRevoked || RevokedAt != null)
		{
			return RefreshTokenStatus.Revoked;
		}

		if (IsInvalidated || InvalidatedAt != null)
		{
			return RefreshTokenStatus.Invalidated;
		}

		return RefreshTokenStatus.Active;
	}


	/// <summary>
	/// Updates the status of the refresh token to the given value.
	/// </summary>
	public void UpdateStatus(RefreshTokenStatus status)
	{
		switch (status)
		{
			case RefreshTokenStatus.Invalidated:
				MarkAsInvalid();
				break;
			case RefreshTokenStatus.Revoked:
				MarkAsRevoked();
				break;
			case RefreshTokenStatus.Expired:
				MarkAsExpired();
				break;
		}
	}


}