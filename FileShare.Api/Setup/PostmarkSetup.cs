namespace FileShare.Api.Setup
{
    public static class PostmarkSetup
    {
        public static void SetupPostmark(this IServiceCollection services, IConfiguration configuration)
        {
            var postmark = configuration.GetSection("Postmark").Get<Postmark>();

            services
                .AddFluentEmail(postmark.FromEmail)
                .AddPostmarkSender(postmark.Key);
        }

        private class Postmark
        {
            public string FromEmail { get; set; }
            public string Key { get; set; }
        }
    }
}