# 01-upgrade-project: Upgrade sk-csharp-console-chat to .NET 10

Update the target framework to net10.0, upgrade NuGet packages (Microsoft.Extensions.Configuration.UserSecrets 6.0.0 → 10.0.5, Microsoft.Extensions.Hosting 6.0.0 → 10.0.5), and fix API compatibility issues including the binary-incompatible `ConfigurationBinder.Get<T>()` calls and the `ConsoleLoggerExtensions.AddConsole()` behavioral change.

**Done when**: Project targets net10.0, all packages are updated, all API issues are resolved, and the solution builds with 0 errors.
