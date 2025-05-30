using news.Config;
using news.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IHackerNewsApiWrapper, HackerNewsApiWrapper>();
builder.Services.Configure<HackerNewsApiConfig>(builder.Configuration.GetSection(HackerNewsApiConfig.Section));
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
