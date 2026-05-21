using ScoreZone.Application.Shared.Results;

namespace ScoreZone.Application.Auth
{
    public interface IAuthService
    {
        Task<AppResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO request);

        Task<AppResult<RegisterResponseDTO>> RegisterAsync(RegisterRequestDTO request);

        Task<AppResult> ResetPasswordAsync(ResetPasswordRequestDTO request);
    }
}