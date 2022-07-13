using Microsoft.Extensions.DependencyInjection;
using ImageApi.Service.Services.Document;
using ImageApi.Service.Services.Account;
using ImageApi.Service.Services.Document.Interface;
using ImageApi.Service.Services.Account.Interface;
using ImageApi.Service.Services.Login;
using ImageApi.Service.Services.Share;
using ImageApi.Service.Services.Login.Interface;
using ImageApi.Service.Services.Share.Interface;

namespace ImageApi.Service
{
    public static class Service
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IShareService, ShareService>();
        }
    }
}