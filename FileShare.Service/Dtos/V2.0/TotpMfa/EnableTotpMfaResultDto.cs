namespace FileShare.Service.Dtos.V2._0.TotpMfa
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