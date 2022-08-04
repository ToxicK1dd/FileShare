namespace FileShare.Service.Dtos.V2._0.Registration
{
    public record RegistrationResultDto
    {
        public RegistrationResultDto(bool successful, string errorMessage)
        {
            Successful = successful;
            ErrorMessage = errorMessage;
        }


        public bool Successful { get; }

        public string ErrorMessage { get; }
    }
}