using System.Text.Json;
using System.Text.Json.Serialization;
using E_commerceTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static E_commerceTask.Shared.Constants.SD;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();




builder.Services.AddCors();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

#endregion





builder.WebHost.UseIIS();
builder.WebHost.UseIISIntegration();
var app = builder.Build();






app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint($"/swagger/{Modules.AdminPanel}/swagger.json", "Website v1");
});
//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet(string.Empty, (context) =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.MapGet("env", async (context) => await context.Response.WriteAsync(app.Environment.EnvironmentName));

app.Run();
