using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ae.Sample.Identity.Ui;
using Ae.Sample.Identity.Ui.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Logging.AddConfiguration(
  builder.Configuration.GetSection("Logging"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
  .AddAppConfiguration(builder.Configuration)
  .AddAppMapper()
  .AddAppServices();

var app = builder.Build();

await app.RunAsync();

