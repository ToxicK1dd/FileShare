﻿using FileShare.Api.Models.V2._0.Login;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Login
{
    public class ChangePasswordModelExample : IExamplesProvider<ChangePasswordModel>
    {
        public ChangePasswordModel GetExamples()
        {
            return new()
            {
                OldPassword = "!Krypton1t3",
                NewPassword = "?Krypton1t3"
            };
        }
    }
}