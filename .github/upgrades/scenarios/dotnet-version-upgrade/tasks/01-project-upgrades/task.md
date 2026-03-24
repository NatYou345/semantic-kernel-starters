# 01-project-upgrades: Update target frameworks and NuGet packages

Update TargetFramework to net10.0 in all 5 project files needing TFM changes (ProductDocumentation, sk_csharp_apim_demo, sk-chatgpt-azure-function, sk-csharp-azure-functions, sk-typescript-console-chat). Update NuGet package references to .NET 10-compatible versions across all projects, including semantic-functions-generator (netstandard2.0, NuGet updates only). Replace the deprecated NuGet package in sk_csharp_apim_demo.

**Done when**: All project files have correct TargetFramework values and updated NuGet package references.
