using ElasticSearchKibana.DbContext;
using ElasticSearchKibana.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Server=localhost;Port=32768;Database=ElasticSearchDb;" +
                      "User Id=postgres;Password=postgres;");
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();