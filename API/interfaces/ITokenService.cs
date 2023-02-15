
using API.Entities;
using API.Services;

namespace API.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}