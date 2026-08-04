

namespace Ecommerce.Identity.Modules.Infrastructure;

public static class IdentityModuleExtensions
{
    public static IServiceCollection AddIdentityModule(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddJwtAuthentication(configuration);

        return services;
    }
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
        // Register Repositories
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        //Custom Services
      services.AddScoped<IidentityUnitOfWork, IdentityUnitOfWork>();
        services.AddHealthChecks()
    .AddDbContextCheck<IdentityContext>("identity-db");
    }
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Configure JWT Bearer options
                var jwt = configuration.GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>()!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwt.SecretKey)),
                    ClockSkew = TimeSpan.Zero

                };

            });
  
        services
    .AddOptions<JwtOptions>()
    .Bind(configuration.GetSection(JwtOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();


        services.AddAuthorization();
    }
    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

        return services;
    }
}
