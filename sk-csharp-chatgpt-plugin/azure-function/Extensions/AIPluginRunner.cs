// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Models;

namespace AIPlugins.AzureFunctions.Extensions;

public class AIPluginRunner : IAIPluginRunner
{
    private readonly ILogger<AIPluginRunner> _logger;
    private readonly Kernel _kernel;

    public AIPluginRunner(Kernel kernel, ILoggerFactory loggerFactory)
    {
        this._kernel = kernel;
        this._logger = loggerFactory.CreateLogger<AIPluginRunner>();
    }


    /// <summary>
    /// Runs a semantic function using the operationID and returns back an HTTP response.
    /// </summary>
    /// <param name="req"></param>
    /// <param name="operationId"></param>
    public async Task<HttpResponseData> RunAIPluginOperationAsync(HttpRequestData req, string operationId)
    {
        KernelArguments arguments = LoadArgumentsFromRequest(req);

        var appSettings = AppSettings.LoadSettings();

        if (!this._kernel.Plugins.TryGetFunction(
            pluginName: appSettings.AIPlugin.NameForModel,
            functionName: operationId,
            out KernelFunction? function))
        {
            HttpResponseData errorResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await errorResponse.WriteStringAsync($"Function {operationId} not found");
            return errorResponse;
        }

        try
        {
            var result = await this._kernel.InvokeAsync(function, arguments);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain;charset=utf-8");
            await response.WriteStringAsync(result.GetValue<string>() ?? string.Empty);
            return response;
        }
        catch (Exception ex)
        {
            HttpResponseData errorResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await errorResponse.WriteStringAsync(ex.Message);
            return errorResponse;
        }
    }

    /// <summary>
    /// Grabs the context variables to send to the semantic function from the original HTTP request.
    /// </summary>
    /// <param name="req"></param>
    protected static KernelArguments LoadArgumentsFromRequest(HttpRequestData req)
    {
        KernelArguments arguments = new KernelArguments();
        foreach (string? key in req.Query.AllKeys)
        {
            if (!string.IsNullOrEmpty(key))
            {
                arguments[key] = req.Query[key];
            }
        }

        // If "input" was not specified in the query string, then check the body
        if (string.IsNullOrEmpty(req.Query.Get("input")))
        {
            // Load the input from the body
            string? body = req.ReadAsString();
            if (!string.IsNullOrEmpty(body))
            {
                arguments["input"] = body;
            }
        }

        return arguments;
    }
}
