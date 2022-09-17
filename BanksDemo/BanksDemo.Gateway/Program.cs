using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Ocelot Tanýmlamalarý

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName.ToLower()}.json");
builder.Services.AddOcelot();
#endregion


var app = builder.Build();
await app.UseOcelot();
app.Run();
