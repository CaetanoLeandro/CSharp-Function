using System;
using Consulta.Func.Ofac.Aplicacao;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = Host
    .CreateDefaultBuilder(args)
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((hostingContext, configBuilder) =>
    {
        var env = hostingContext.HostingEnvironment;

        IConfiguration configuracoes =
        configBuilder
         .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables()
         .Build();

    })
    .ConfigureServices((appBuilder, services) =>
    {
        var config = appBuilder.Configuration;

        services.RegistrarServicos(config);
    });

builder.Build().Run();