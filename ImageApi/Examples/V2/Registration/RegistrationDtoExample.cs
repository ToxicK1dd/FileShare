using ImageApi.Service.Dto.Registration;
using Swashbuckle.AspNetCore.Filters;

namespace ImageApi.Examples.V2.Registration
{
    public class RegistrationDtoExample : IExamplesProvider<RegistrationDto>
    {
        public RegistrationDto GetExamples()
        {
            return new("Superman", "superman@kryptonmail.space", "!Krypton1t3");
        }
    }
}