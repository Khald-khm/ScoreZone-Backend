using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Auth
{
    public record LoginRequestDTO(
        string phone,
        string password
    );


    public record LoginResponseDTO(
        string id,
        string accesToken,
        string refreshToken,
        string phone,
        IList<string> roles
    );

    public record RegisterRequestDTO(
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDate,
        // string username,
        string password,
        string phone,
        string? email,
        City city,
        string address,
        Roles role
    );

    public record RegisterResponseDTO(
        string id,
        string accessToken,
        string refreshToken,
        IList<string> roles
    );

    public record ResetPasswordRequestDTO(
        string phone,
        string newPassword
    );
}