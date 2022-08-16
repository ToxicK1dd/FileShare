using FileShare.Api.Models.V2._0.Password;
using Swashbuckle.AspNetCore.Filters;

namespace FileShare.Api.Examples.V2._0.Password
{
    public class ResetPasswordModelExample : IExamplesProvider<ResetPasswordModel>
    {
        public ResetPasswordModel GetExamples()
        {
            return new()
            {
                Email = "superman@kryptonmail.space",
                Password = "?Krypton1t3",
                ConfirmPassword = "?Krypton1t3",
                ResetToken = "CfDJ8L7tx6RWGKpCuu9yZvtLj9wY6iPQHghCqJfI5VLTY+iNK+xtVDZv9lCTe3XMNVY5DyZv3kk0tBQ+502cy1DYvmELnZQ9xWGnkaS2hiG+lodvYPwgYfHOZ2y4O9+fTZ2xc5WPpF/oxlDSIXx4MV2bV/oqaIFgJZ8DxBokXrqvOdei9pFqadSklCf6N796ALciHLy4bcFVBEuXyeWHkm9iL+lnB480/oQ1jqOYQqX9K94W"
            };
        }
    }
}