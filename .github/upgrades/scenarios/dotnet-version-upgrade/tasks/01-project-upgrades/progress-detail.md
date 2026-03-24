# 01-project-upgrades: Progress Detail

## Changes Made

### TFM Updates (5 projects → net10.0)
- `ProductDocumentation.csproj`: net8.0 → net10.0
- `sk_csharp_apim_demo.csproj`: net8.0 → net10.0
- `sk-chatgpt-azure-function.csproj`: net8.0 → net10.0
- `sk-csharp-azure-functions.csproj`: net8.0 → net10.0
- `sk-typescript-console-chat.csproj`: net8.0 → net10.0

### TFM Kept (1 project)
- `semantic-functions-generator.csproj`: stays netstandard2.0 (source generator, no TFM change needed)

### NuGet Package Updates
| Project | Package | Old Version | New Version |
|---------|---------|-------------|-------------|
| semantic-functions-generator | Newtonsoft.Json | 13.0.3 | 13.0.4 |
| sk_csharp_apim_demo | Azure.Identity | 1.13.2 (deprecated) | 1.19.0 |
| sk_csharp_apim_demo | Microsoft.Extensions.Logging.Console | 6.0.0 | 10.0.5 |
| sk_csharp_apim_demo | Microsoft.Extensions.Logging.Debug | 6.0.0 | 10.0.5 |
| sk-chatgpt-azure-function | Microsoft.Extensions.Configuration.UserSecrets | 6.0.1 | 10.0.5 |
| sk-chatgpt-azure-function | Microsoft.Azure.Functions.Worker | 1.18.0 | 2.51.0 |
| sk-chatgpt-azure-function | Microsoft.Azure.Functions.Worker.Extensions.Http | 3.0.13 | 3.3.0 |
| sk-chatgpt-azure-function | Microsoft.Azure.Functions.Worker.Sdk | 1.12.0 | 2.0.7 |
| sk-csharp-azure-functions | Microsoft.Extensions.Configuration.UserSecrets | 6.0.1 | 10.0.5 |
| sk-csharp-azure-functions | Microsoft.Azure.Functions.Worker | 1.18.0 | 2.51.0 |
| sk-csharp-azure-functions | Microsoft.Azure.Functions.Worker.Extensions.Http | 3.0.13 | 3.3.0 |
| sk-csharp-azure-functions | Microsoft.Azure.Functions.Worker.Sdk | 1.12.0 | 2.0.7 |

### Packages Removed
| Project | Package | Reason |
|---------|---------|--------|
| sk-chatgpt-azure-function | Microsoft.Azure.WebJobs.Extensions.OpenApi | Functionality included with framework reference |
| sk-csharp-azure-functions | Microsoft.Azure.WebJobs.Extensions.OpenApi | Functionality included with framework reference |

### Already on net10.0 (no changes needed)
- `sk-csharp-console-chat.csproj`
- `sk-csharp-hello-world.csproj`
