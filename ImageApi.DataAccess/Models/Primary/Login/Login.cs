﻿namespace ImageApi.DataAccess.Models.Primary.Login
{
    public class Login : BaseEntity
    {
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Name used for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password used for authentication
        /// </summary>
        public string Password { get; set; }
    }
}