using Microsoft.Extensions.DependencyInjection;
using ImageApi.Service.Services.Document;
using ImageApi.Service.Services.Account;
using ImageApi.Service.Services.Document.Interface;
using ImageApi.Service.Services.Account.Interface;
using ImageApi.Service.Services.Login;
using ImageApi.Service.Services.Share;
using ImageApi.Service.Services.Login.Interface;
using ImageApi.Service.Services.Share.Interface;
using ImageApi.Service.Services.Registration;
using ImageApi.Service.Services.Registration.Interface;
using ImageApi.Service.Services.Token;
using ImageApi.Service.Services.Token.Interface;

namespace ImageApi.Service
{
    public static class Service
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IShareService, ShareService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}