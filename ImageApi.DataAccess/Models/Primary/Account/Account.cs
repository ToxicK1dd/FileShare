namespace ImageApi.DataAccess.Models.Primary.Account
{
    public class Account : BaseEntity
    { 
        public bool Enabled { get; set; }

        public string Email { get; set; }    
    }
}