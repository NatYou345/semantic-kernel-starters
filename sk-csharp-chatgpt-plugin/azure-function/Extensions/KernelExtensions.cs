// Copyright (c) Microsoft. All rights reserved.

using Microsoft.SemanticKernel;

namespace AIPlugins.AzureFunctions.Extensions;

public static class KernelExtensions
{
    public static KernelPlugin ImportPromptsFromDirectory(
        this Kernel kernel, string pluginName, string promptDirectory)
    {
        return kernel.ImportPluginFromPromptDirectory(promptDirectory, pluginName);
    }
}
