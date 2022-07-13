namespace ImageApi.DataAccess.Models.Primary.RefreshToken
{
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// Login foreign key
        /// </summary>
        public Guid LoginId { get; set; }

        /// <summary>
        /// Login navigation property
        /// </summary>
        public Login.Login Login { get; set; }

        /// <summary>
        /// Token used for optaining a new JWT
        /// </summary>
        public string Token { get; set; }
    }
}