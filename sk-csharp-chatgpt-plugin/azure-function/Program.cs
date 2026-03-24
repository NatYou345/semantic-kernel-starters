// Copyright (c) Microsoft. All rights reserved.

using AIPlugins.AzureFunctions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Models;

const string DefaultSemanticFunctionsFolder = "Prompts";
string semanticFunctionsFolder = Environment.GetEnvironmentVariable("SEMANTIC_SKILLS_FOLDER") ?? DefaultSemanticFunctionsFolder;

var builder = FunctionsApplication.CreateBuilder(args);

// Enable Application Insights telemetry
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services
    .AddScoped<Kernel>((providers) =>
    {
        // This will be called each time a new Kernel is needed

        // Register your AI Providers...
        var appSettings = AppSettings.LoadSettings();
        var kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.Services.AddLogging(c => c.AddConsole().AddDebug());
        kernelBuilder.WithChatCompletionService(appSettings.Kernel);

        Kernel kernel = kernelBuilder.Build();

        // Load your semantic functions...
        kernel.ImportPromptsFromDirectory(appSettings.AIPlugin.NameForModel, semanticFunctionsFolder);

        return kernel;
    })
    .AddScoped<IAIPluginRunner, AIPluginRunner>();

builder.Build().Run();
