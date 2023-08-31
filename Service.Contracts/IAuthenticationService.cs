using Common.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Application.Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    }
}
