using Model.Entity;

namespace Service.Interface
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
