using ArtwiseDatabase.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace ArtwiseAPI.Extensions;

/// <summary>
/// Defines extension methods for <see cref="IServiceCollection"/>.
/// </summary>
internal static class IServiceCollectionExt
{
    /// <summary>
    /// Adds the default authentication and authorization policies used by the Artwise API.
    /// </summary>
    /// <param name="serviceCollection">This service collection.</param>
    /// <param name="privateKey">The private key to be used for signature validation.</param>
    /// <param name="addSwaggerAuth"><see langword="true"/> to add an authentication button on Swagger, <see langword="false"/> otherwise.</param>
    /// <returns>This service collection with the policies added.</returns>
    public static IServiceCollection AddArtwiseAuth(this IServiceCollection serviceCollection, byte[] privateKey, bool addSwaggerAuth = true)
    {
        if (addSwaggerAuth)
        {
            // Add Swagger authentication button
            serviceCollection.AddSwaggerGen(swaggerOptions =>
            {
                var apiSecurityScheme = new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string in the format: `bearer jwt-token-here`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference()
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                swaggerOptions.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, apiSecurityScheme);

                // OpenApiSecurityRequirement is a Dictionary<OpenApiSecurityScheme, IList<string>>
                // Any scheme other than "oauth2" or "openIdConnect" must contain an empty IList
                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement() { [apiSecurityScheme] = Array.Empty<string>() });
            });
        }

        serviceCollection
            .AddAuthorization(authOptions =>
            {
                // Require all users to be authenticated
                authOptions.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                // Add authorization policies
                // Based on the name of UserPermissions enums (except "Blocked")
                foreach (var permission in Enum.GetValues<UserType>())
                    authOptions.AddPolicy(permission.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, permission.ToString()));
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return serviceCollection;
    }
}