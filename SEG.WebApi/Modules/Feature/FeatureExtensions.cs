using Microsoft.AspNetCore.Mvc;

namespace SEG.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            string myPolicy = "policyApiConfiguracion";

            services.AddCors(options =>
                options.AddPolicy(myPolicy, builder => builder.WithOrigins(configuration["Config:OriginCors"])
                                                                                        .AllowAnyOrigin()
                                                                                        .AllowAnyHeader()
                                                                                        .AllowAnyMethod()));
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            return services;
        }
    }
}
