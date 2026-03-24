# 02-code-fixes: Progress Detail

## Changes Made

### Azure Functions V2 Migration (2 projects)

**sk-csharp-azure-functions/Program.cs:**
- Replaced `new HostBuilder()` with `FunctionsApplication.CreateBuilder(args)`
- Removed `ConfigureAppConfiguration` callback (auto-loaded in V2)
- Removed `ConfigureServices` callback wrapper — services registered via `builder.Services` directly
- Added Application Insights telemetry (`AddApplicationInsightsTelemetryWorkerService` + `ConfigureFunctionsApplicationInsights`)
- Added `using Microsoft.Azure.Functions.Worker;`
- Updated `Main()` to `Main(string[] args)` for builder pattern

**sk-csharp-chatgpt-plugin/azure-function/Program.cs:**
- Replaced `new HostBuilder()` with `FunctionsApplication.CreateBuilder(args)`
- Removed `ConfigureServices` callback wrapper — services registered via `builder.Services` directly
- Added Application Insights telemetry
- Added `using Microsoft.Azure.Functions.Worker;`

### Application Insights Packages Added (2 projects)
| Package | Version |
|---------|---------|
| Microsoft.Azure.Functions.Worker.ApplicationInsights | 2.50.0 |
| Microsoft.ApplicationInsights.WorkerService | 2.23.0 |

### API Issues Addressed
- **Api.0003 (HostBuilder behavioral change)**: Resolved by migrating to `FunctionsApplication.CreateBuilder` (V2 model)
- **Api.0003 (ConsoleLoggerExtensions.AddConsole)**: Behavioral change only (no code change required — method signature unchanged)
- **Api.0003 (System.Uri)**: Behavioral change only in `AiPluginJson.cs` (no code change required)
- **Api.0001 (ConfigurationBinder.Get<T>)**: Binary incompatibility — will be verified during build validation
