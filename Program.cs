using System.Web.Http;
using yatracub;

 var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Add services to the container.s
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // calling ConfigureServices method

var app = builder.Build();

 // calling Configure method

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
startup.Configure(app, builder.Environment);
//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
