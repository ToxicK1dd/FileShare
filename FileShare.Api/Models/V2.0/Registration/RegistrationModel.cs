﻿using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Registration
{
    /// <summary>
    /// Model for registering a new user.
    /// </summary>
    public record RegistrationModel
    {
        public RegistrationModel() { }

        public RegistrationModel(string username, string email, string password, string confirmPassword)
        {
            Username = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }


        [Required(ErrorMessage = "{0} is a mandatory field")]
        public string Username { get; init; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [EmailAddress]
        public string Email { get; init; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required(ErrorMessage = "{0} is a mandatory field")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords doesn't match.")]
        public string ConfirmPassword { get; init; }
    }
}