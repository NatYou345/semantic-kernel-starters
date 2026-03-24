using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

string[] scopes = new string[] { "https://cognitiveservices.azure.com/.default" };
var credential = new InteractiveBrowserCredential();
var requestContext = new TokenRequestContext(scopes);
var accessToken = await credential.GetTokenAsync(requestContext);

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "<Subscript key>");

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddLogging(c => c
    .SetMinimumLevel(LogLevel.Warning)
    .AddConsole()
    .AddDebug());
kernelBuilder.AddAzureOpenAIChatCompletion(
    deploymentName: "text-davinci-003",
    endpoint: "https://apim...azure-api.net/",
    credentials: credential,
    httpClient: httpClient
);
Kernel kernel = kernelBuilder.Build();

var skillsDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "skills");
var plugin = kernel.ImportPluginFromPromptDirectory(Path.Combine(skillsDirectory, "FunSkill"), "FunSkill");

var arguments = new KernelArguments();
arguments["input"] = "Time travel to dinosaur age";
arguments["style"] = "Wacky";

var result = await kernel.InvokeAsync(plugin["Joke"], arguments);

Console.WriteLine(result);

httpClient.Dispose();
