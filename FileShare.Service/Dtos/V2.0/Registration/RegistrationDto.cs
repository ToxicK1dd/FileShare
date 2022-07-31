namespace FileShare.Service.Dtos.V2._0.Registration
{
    public record RegistrationResultDto
    {
        public RegistrationResultDto(Guid accountId, Guid loginId)
        {
            AccountId = accountId;
            LoginId = loginId;
        }


        public Guid AccountId { get; }

        public Guid LoginId { get; }
    }
}