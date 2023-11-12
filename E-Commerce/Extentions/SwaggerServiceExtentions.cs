using Microsoft.OpenApi.Models;

namespace E_Commerce.Extentions
{
    public static class SwaggerServiceExtentions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiDemo", Version = "v1" });
                
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization header using the Bearer scheme. Example \"Authorization:Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);


                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securityScheme,new []{ "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
            });


            return services;
        }
    }
}
