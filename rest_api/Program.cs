using DAL.Interfaces;
using DAL.PostgreSQL.EfCore;
using DAL.PostgreSQL.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using rest_api.Infrastructure.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApiVersioning(
            setup =>
            {
                setup.DefaultApiVersion = new Asp.Versioning.ApiVersion(2, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            })
            .AddApiExplorer(
            opts => {
                opts.GroupNameFormat = "'v'VVV";
                opts.SubstituteApiVersionInUrl = true;
            });

        builder.Services.Configure<RouteOptions>(opts => { opts.LowercaseUrls = true; });

        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        builder.Services.AddSwaggerGen(
                 options =>
                 {
                     // add a custom operation filter which sets default values
                     options.OperationFilter<SwaggerDefaultValues>();
                     var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                     var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                     // integrate xml comments
                     options.IncludeXmlComments(filePath);
                 });



        // Регистрируем зависимости
        builder.Services
             .AddDbContext<ChainOfShopsLuckContext>(
             s => s.UseNpgsql("Host=localhost;Port=5432;Database=chain_of_shops_luck;Username=admin;Password=secret"));
        // TODO: вынести пароль

        builder.Services.AddSingleton<IShopRepository, ShopRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                opts => {
                    var descr = app.DescribeApiVersions();

                    foreach (var description in descr)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        
                        opts.SwaggerEndpoint(url, name);
                    }
                });
        }


        app.NewVersionedApi();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}