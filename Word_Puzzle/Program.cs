using Microsoft.Extensions.Hosting;
using Puzzle_API.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Serilog.Configuration;

var builder = WebApplication.CreateBuilder(args);
var AllowOrigin = "AllowOrigin";

// Add services to the container.////
//
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog((ctx, lc) => lc
      .WriteTo.File("logs/log.txt"));
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddDbContext<Puzzle_API.Data.DataContext>();
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.MaxDepth = 64;
    options.JsonSerializerOptions.IncludeFields = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("PuzzleGame", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.Name = ".PuzzleGameUser";
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});

//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/Puzzle_APIlogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

var app = builder.Build();
app.UseCors("PuzzleGame");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSession();

app.Run();
