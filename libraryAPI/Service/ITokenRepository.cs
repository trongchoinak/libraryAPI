using Microsoft.AspNetCore.Identity;

namespace libraryAPI.Service
{
    public interface ITokenRepository
    {

        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
