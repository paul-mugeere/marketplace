using Marketplace.Api.Contracts;
using Marketplace.Api.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>c.SwaggerDoc("v1",new OpenApiInfo()
{
    Title = "Marketplace API",
    Version = "V1"
}));

builder.Services.AddScoped<IEntityStore, RavenDbEntityStore>();
builder.Services.AddScoped<IHandleCommand<ClassifiedAds.V1.Create>,CreateClassifiedAdCommandHandler>();
builder.Services.AddScoped<IHandleCommand<object>,ClassifiedAdsApplicationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Marketplace API"));
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();