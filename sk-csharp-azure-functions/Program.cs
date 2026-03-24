using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;

namespace KernelHttpServer;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = FunctionsApplication.CreateBuilder(args);

        // Enable Application Insights telemetry
        builder.Services
            .AddApplicationInsightsTelemetryWorkerService()
            .ConfigureFunctionsApplicationInsights();

        builder.Services.AddSingleton<IOpenApiConfigurationOptions>(_ => s_apiConfigOptions);
        builder.Services.AddTransient<Kernel>((provider) => CreateKernel(provider));

        // return JSON with expected lowercase naming
        builder.Services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        builder.Build().Run();
    }

    private static Kernel CreateKernel(IServiceProvider provider)
    {
        var kernelSettings = KernelSettings.LoadSettings();

        var kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.Services.AddLogging(c => c
            .SetMinimumLevel(kernelSettings.LogLevel ?? LogLevel.Warning)
            .AddConsole()
            .AddDebug());
        kernelBuilder.WithCompletionService(kernelSettings);

        return kernelBuilder.Build();
    }

    private static readonly OpenApiConfigurationOptions s_apiConfigOptions = new()
    {
        Info = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "Semantic Kernel Azure Functions Starter",
            Description = "Azure Functions starter application for the [Semantic Kernel](https://github.com/microsoft/semantic-kernel).",
            Contact = new OpenApiContact()
            {
                Name = "Issues",
                Url = new Uri("https://github.com/microsoft/semantic-kernel-starters/issues"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("https://github.com/microsoft/semantic-kernel-starters/blob/main/LICENSE"),
            }
        },
        Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
        OpenApiVersion = OpenApiVersionType.V2,
        ForceHttps = false,
        ForceHttp = false,
    };
}
