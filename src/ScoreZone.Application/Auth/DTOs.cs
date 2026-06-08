using ScoreZone.Domain.Shared.Enum;

namespace ScoreZone.Application.Auth
{
    public record LoginRequestDTO(
        string username,
        string password
    );


    public record LoginResponseDTO(
        string id,
        string accesToken,
        string refreshToken,
        string username,
        IList<string> roles
    );

    public record RegisterRequestDTO(
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDate,
        string username,
        string password,
        string phone,
        string email,
        Roles role
    );

    public record RegisterResponseDTO(
        string id,
        string accessToken,
        string refreshToken,
        IList<string> roles
    );

    public record ResetPasswordRequestDTO(
        string username,
        string newPassword
    );
}