namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    { 
        /// <summary>
        /// Toggles the ability to authenticate
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Mail used for sending messages
        /// </summary>
        public string Email { get; set; }    
    }
}