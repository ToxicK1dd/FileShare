namespace ImageApi.DataAccess.Models.Primary.LoginDetail
{
    public class LoginDetail : BaseEntity
    {
        /// <summary>
        /// Login forein key
        /// </summary>
        public Guid LoginId { get; set; }

        /// <summary>
        /// Login navigation property
        /// </summary>
        public Login.Login Login { get; set; }

        /// <summary>
        /// Indicates if the login attempt was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates what type of device attempted to login
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// The time of the login attemt
        /// </summary>
        public DateTimeOffset Time { get; set; }
    }

    public enum DeviceType
    {
        Desktop = 1, // Web browser
        Mobile = 2, // Web browser
        MobileApp = 3,
    }
}