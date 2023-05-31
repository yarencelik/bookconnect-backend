using BookConnect.API;
using BookConnect.API.Extensions;
using BookConnect.Application;
using BookConnect.Infrastructure;
using BookConnect.Infrastructure.Persistence.Seeds;
using BookConnect.Infrastructure.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddNewtonsoftJson(opt => {
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAPIServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration["ConnectionStrings:DB"]!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if(args.Length > 0)
{
    var seed = args.Any(x => x == "seed:user_data");
    if(seed)
        app.SeedUserData(builder.Configuration, new PasswordService(builder.Configuration), new ShelfService());
}

app.UseHealthChecks("/_health", new HealthCheckOptions 
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// await app.AddMigrations();

app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
