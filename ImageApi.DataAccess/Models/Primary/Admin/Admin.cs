namespace ImageApi.DataAccess.Models.Primary.Admin
{
    public class Admin : User.User
    {
        /// <summary>
        /// The level of power the admin has
        /// </summary>
        public AdminRoleType RoleType { get; set; }
    }

    public enum AdminRoleType
    {
        Global = 1,
        Support = 2
    }
}