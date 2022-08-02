using FileShare.Api.Models.V2._0.Registration;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Registration
{
    public class RegistrationModelExample : IExamplesProvider<RegistrationModel>
    {
        public RegistrationModel GetExamples()
        {
            return new("Superman", "superman@kryptonmail.space", "!Krypton1t3");
        }
    }
}