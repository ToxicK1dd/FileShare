namespace FileShare.Service.Dtos.RefreshToken
{
    public record RefreshTokenDto
    {
        public RefreshTokenDto() { }

        public RefreshTokenDto(Guid id, DateTimeOffset expires, DateTimeOffset revoked, bool isRevoked)
        {
            Id = id;
            Expires = expires;
            Revoked = revoked;
            IsRevoked = isRevoked;
        }


        public Guid Id { get; init; }


        public DateTimeOffset Expires { get; init; }

        public bool IsExpired { get => Expires < DateTimeOffset.UtcNow; }


        public DateTimeOffset Revoked { get; init; }

        public bool IsRevoked { get; init; }
    }
}