using Fiap.Api.Coletas.Models;

namespace Fiap.Api.Coletas.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(String username, String password);
    }
}
