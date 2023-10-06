using Web;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
// calling ConfigureServices method
startup.ConfigureServices(builder.Services);

var app = builder.Build();
// calling Configure method
startup.Configure(app, builder.Environment);