using System.Security.Claims;
using ScoreZone.Application.Shared.Interfaces;

namespace ScoreZone.API.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _userAccessor;

        public CurrentUser(
            IHttpContextAccessor userAccessor
        )
        {
            _userAccessor = userAccessor;
        }

        public string? identityId => _userAccessor.HttpContext?.User.FindFirstValue("IdentityId");
        public Guid? userId 
        { 
            get 
            { 
                var id = _userAccessor.HttpContext?.User.FindFirstValue("UserId");
                return Guid.TryParse(id, out var result) ? result : (Guid?) null;
            }
        }

        public string? phoneNumber => _userAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        public string? role => _userAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        // public string roles => _userAccessor.HttpContext.User.IsInRole(role);

        // public bool IsInRole(string role)
        // {
        //     return _httpContextAccessor.HttpContext?
        //         .User?
        //         .IsInRole(role) ?? false;
        // }
    }
}