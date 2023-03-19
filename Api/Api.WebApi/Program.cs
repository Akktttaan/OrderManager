using Dal;
using WebApi.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
builder.Services
    .AddConfiguration(builder.Configuration)
    .AddUnitOfWork(builder.Configuration)
    .AddServices()
    .AddAutoMapper()
    .AddCors(options => options.AddPolicy("CorsPolicy",
        builder => builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            .AllowCredentials()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    SeedData.Initialize(services);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the database.");
}

app.Run();