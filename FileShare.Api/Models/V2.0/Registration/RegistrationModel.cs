﻿using System.ComponentModel.DataAnnotations;

namespace FileShare.Api.Models.V2._0.Registration
{
    /// <summary>
    /// Model for registering a new user.
    /// </summary>
    public record RegistrationModel
    {
        [Required]
        public string Username { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords doesn't match.")]
        public string ConfirmPassword { get; init; }
    }
}