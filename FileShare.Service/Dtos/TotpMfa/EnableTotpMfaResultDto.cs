namespace FileShare.Service.Dtos.TotpMfa
{
    public record TotpMfaCodesResultDto
    {
        public TotpMfaCodesResultDto() { }

        public TotpMfaCodesResultDto(string key, IEnumerable<string> recoveryCodes)
        {
            Key = key;
            RecoveryCodes = recoveryCodes;
        }


        public string Key { get; init; }

        public IEnumerable<string> RecoveryCodes { get; init; }
    }
}