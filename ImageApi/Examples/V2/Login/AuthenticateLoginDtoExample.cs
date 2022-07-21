using ImageApi.Service.Dto.Login;
using Swashbuckle.AspNetCore.Filters;

namespace ImageApi.Examples.V2.Login
{
    public class AuthenticateLoginDtoExample : IExamplesProvider<AuthenticateLoginDto>
    {
        public AuthenticateLoginDto GetExamples()
        {
            return new("Superman", "!Krypton1t3");
        }
    }
}