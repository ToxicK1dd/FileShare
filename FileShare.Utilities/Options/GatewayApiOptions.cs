namespace FileShare.Utilities.Options
{
    /// <summary>
    /// Options for calling the sms gateway api.
    /// </summary>
    public class GatewayApiOptions
    {
        /// <summary>
        /// Host base url for the api.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authorization key for the api.
        /// </summary>
        public string Key { get; set; }
    }
}