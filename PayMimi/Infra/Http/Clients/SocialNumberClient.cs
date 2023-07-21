using Refit;

namespace PayMimi.Infra.Http.Clients;

public interface ISocialNumberClient
{
    [Get("/social-number/{socialNumber}")]
    Task<SocialNumberValidationResponse> IsValid(string socialNumber);
}

public record SocialNumberValidationResponse(string SocialNumber, bool Valid, DateTime ValidatedAt);