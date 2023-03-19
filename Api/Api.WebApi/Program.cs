using Api.Infrastructure.MiddleWares;
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
    .AddCorsPolicy()
    .AddCustomerLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(uiBind =>
        uiBind.SwaggerEndpoint
            ("/swagger/v1/swagger.json", "OrderManager Api")
    );
}
else
{
    app.UseHsts();
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

using var scope = app.Services.CreateScope();
SeedData.Initialize(scope.ServiceProvider);

app.Run();