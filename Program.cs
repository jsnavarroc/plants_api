using _Net.Data;
using _Net.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Cors 
builder.Services.AddCors();

// Add repositories.
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Add Db services
builder.Services.AddDbContext<ApiDBContext>(options =>
{
    options.UseInMemoryDatabase("TodoDB");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IConfiguration configuration = builder.Configuration;
    string origins = configuration.GetValue<string>("CORS_ORIGINS") ?? "";
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins));
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
