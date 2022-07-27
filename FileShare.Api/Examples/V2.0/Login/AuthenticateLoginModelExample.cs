using FileShare.Models.V2._0.Login;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Examples.V2._0.Login
{
    public class AuthenticateLoginModelExample : IExamplesProvider<AuthenticateLoginModel>
    {
        public AuthenticateLoginModel GetExamples()
        {
            return new("Superman", "!Krypton1t3");
        }
    }
}