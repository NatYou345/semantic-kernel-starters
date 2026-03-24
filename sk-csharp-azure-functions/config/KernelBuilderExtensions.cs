using Microsoft.SemanticKernel;

internal static class KernelBuilderExtensions
{
    /// <summary>
    /// Adds a completion service to the list. It can be either an OpenAI or Azure OpenAI backend service.
    /// </summary>
    /// <param name="kernelBuilder"></param>
    /// <param name="kernelSettings"></param>
    /// <exception cref="ArgumentException"></exception>
    internal static IKernelBuilder WithCompletionService(this IKernelBuilder kernelBuilder, KernelSettings kernelSettings)
    {
        switch (kernelSettings.ServiceType.ToUpperInvariant())
        {
            case ServiceTypes.AzureOpenAI when kernelSettings.EndpointType == EndpointTypes.TextCompletion:
                kernelBuilder.AddAzureOpenAIChatCompletion(deploymentName: kernelSettings.DeploymentOrModelId, endpoint: kernelSettings.Endpoint, apiKey: kernelSettings.ApiKey, serviceId: kernelSettings.ServiceId);
                break;
            case ServiceTypes.AzureOpenAI when kernelSettings.EndpointType == EndpointTypes.ChatCompletion:
                kernelBuilder.AddAzureOpenAIChatCompletion(deploymentName: kernelSettings.DeploymentOrModelId, endpoint: kernelSettings.Endpoint, apiKey: kernelSettings.ApiKey, serviceId: kernelSettings.ServiceId);
                break;
            case ServiceTypes.OpenAI when kernelSettings.EndpointType == EndpointTypes.TextCompletion:
                kernelBuilder.AddOpenAIChatCompletion(modelId: kernelSettings.DeploymentOrModelId, apiKey: kernelSettings.ApiKey, orgId: kernelSettings.OrgId, serviceId: kernelSettings.ServiceId);
                break;
            case ServiceTypes.OpenAI when kernelSettings.EndpointType == EndpointTypes.ChatCompletion:
                kernelBuilder.AddOpenAIChatCompletion(modelId: kernelSettings.DeploymentOrModelId, apiKey: kernelSettings.ApiKey, orgId: kernelSettings.OrgId, serviceId: kernelSettings.ServiceId);
                break;
            default:
                throw new ArgumentException($"Invalid service type value: {kernelSettings.ServiceType}");
        }

        return kernelBuilder;
    }
}
