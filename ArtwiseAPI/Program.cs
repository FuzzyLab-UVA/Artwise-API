using ArtwiseAPI.Common;
using ArtwiseAPI.Extensions;
using ArtwiseAPI.SwaggerDocumentation;
using ArtwiseDatabase.Extensions;
using Kotz.DependencyInjection.Extensions;

namespace ArtwiseAPI;

internal sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(x => x.OperationFilter<SwaggerResponseDocumentationFilter>())
            .AddRouting()
            .RegisterServices()
            .AddArtwiseDb()
            .AddArtwiseAuth(builder.Configuration.GetValue<byte[]>(ApiConstants.JwtAppSetting)!);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}