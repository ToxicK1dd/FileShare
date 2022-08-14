using FileShare.Api.Models.V2._0.Registration;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Registration
{
    public class RegistrationModelExample : IExamplesProvider<RegistrationModel>
    {
        public RegistrationModel GetExamples()
        {
            return new()
            {
                Username = "Superman",
                Email = "superman@kryptonmail.space",
                Password = "!Krypton1t3",
                ConfirmPassword = "!Krypton1t3"
            };
        }
    }
}