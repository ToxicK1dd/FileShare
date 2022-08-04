namespace FileShare.Service.Dtos.V2._0.Registration
{
    public record RegistrationResultDto
    {
        public RegistrationResultDto(Guid userId, Guid loginId)
        {
            UserId = userId;
            LoginId = loginId;
        }


        public Guid UserId { get; }

        public Guid LoginId { get; }
    }
}