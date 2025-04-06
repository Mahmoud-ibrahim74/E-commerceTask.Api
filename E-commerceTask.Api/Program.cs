using System.Text.Json;
using System.Text.Json.Serialization;
using E_commerceTask.Application.Interfaces;
using E_commerceTask.Infrastructure.Data;
using E_commerceTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static E_commerceTask.Shared.Constants.SD;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();


builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
}
    );




#region DbContextServices

builder.Services.AddDbContext<ECommerceTaskContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString(Shared.EcommerceDbConnection),
     b => b.MigrationsAssembly(typeof(ECommerceTaskContext).Assembly.FullName)).UseLazyLoadingProxies());
#endregion

#region Swagger config

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc(Modules.AdminPanel, new OpenApiInfo
    {
        Title = $"{Shared.EcommerceApp} {Modules.AdminPanel}",
        Version = Shared.AppVersion
    });
});


#endregion

#region DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion



var httpPort = builder.Configuration.GetValue<int>("KestrelServer:Http.Port");
var httpsPort = builder.Configuration.GetValue<int>("KestrelServer:Https.Port");
var httpsCertificateFilePath = builder.Configuration.GetValue<string>("KestrelServer:Https.CertificationFilePath");
var httpsCertificatePassword = builder.Configuration.GetValue<string>("KestrelServer:Https.CertificationPassword");
builder.WebHost.UseIIS();
builder.WebHost.UseIISIntegration();
var app = builder.Build();

#region To take an instance from specific repository

var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopedFactory!.CreateScope();

#endregion




app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint($"/swagger/{Modules.AdminPanel}/swagger.json", "Website v1");
});
//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{

    //app.UseExceptionHandler();
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseSerilogRequestLogging();
app.MapGet(string.Empty, (context) =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.MapGet("env", async (context) => await context.Response.WriteAsync(app.Environment.EnvironmentName));

try
{
    app.Run();
}
catch (Exception ex)
{
}
finally
{
}