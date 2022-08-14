namespace FileShare.Service.Services.Password.Interface
{
    public interface IPasswordService
    {
        /// <summary>
        /// Change the password for the current user.
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(string oldPassword, string newPassword);

        /// <summary>
        /// Generate a password reset token for the user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> RequestResetPasswordAsync(string email);

        /// <summary>
        /// Reset the password for the user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="token"></param>
        /// <returns><see langword="true"/> if the arguments are valid, otherwise <see langword="false"/>.</returns>
        Task<bool> ConfirmResetPasswordAsync(string email, string password, string confirmPassword, string token);
    }
}