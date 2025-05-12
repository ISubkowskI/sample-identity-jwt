using Ae.Sample.Identity.Ui.Profiles;
using Ae.Sample.Identity.Ui.Services;
using Ae.Sample.Identity.Ui.Settings;

namespace Ae.Sample.Identity.Ui.Extensions
{
    public static class WebAsmExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services,
             IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(config);

            services.Configure<AppOptions>(
                config.GetSection(AppOptions.App));
            services.Configure<IdentityStorageApiOptions>(
                config.GetSection(IdentityStorageApiOptions.IdentityStorageApi));
            services.Configure<IdentityApiOptions>(
                config.GetSection(IdentityApiOptions.IdentityApi));

            return services;
        }

        public static IServiceCollection AddAppMapper(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAutoMapper(m =>
            {
                m.AddProfile<UiDataProfile>();
            });
            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddHttpClient<IIdentityStorageClient, IdentityStorageClient>();
            services.AddHttpClient<IIdentityClient, IdentityClient>();

            return services;
        }
    }
}
