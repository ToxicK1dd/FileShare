using FileShare.Api.Models.V2._0.Login;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Login
{
    public class AuthenticateTotpLoginModelExample : IExamplesProvider<AuthenticateTotpLoginModel>
    {
        public AuthenticateTotpLoginModel GetExamples()
        {
            return new("Superman", "!Krypton1t3", "261978");
        }
    }
}