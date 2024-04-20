using APICatalogo.Domain.Entities;

namespace APICatalogo.Application.Services.Token;

public interface ITokenService {

    Task<string> GenerateAccessToken(ApplicationUser user);

}
