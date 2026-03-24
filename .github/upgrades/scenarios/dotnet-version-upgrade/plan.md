# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade sk-csharp-console-chat from net8.0 to net10.0
**Scope**: 1 project, ~287 LOC, 4 NuGet packages (2 need upgrade), 3 API issues

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 1 project, currently on net8.0, low difficulty, clear dependency structure.

## Tasks

### 01-upgrade-project: Upgrade sk-csharp-console-chat to .NET 10

Update the target framework to net10.0, upgrade NuGet packages (Microsoft.Extensions.Configuration.UserSecrets 6.0.0 → 10.0.5, Microsoft.Extensions.Hosting 6.0.0 → 10.0.5), and fix API compatibility issues including the binary-incompatible `ConfigurationBinder.Get<T>()` calls and the `ConsoleLoggerExtensions.AddConsole()` behavioral change.

**Done when**: Project targets net10.0, all packages are updated, all API issues are resolved, and the solution builds with 0 errors.

---

### 02-validate-upgrade: Validate build and run tests

Build the full solution to confirm zero compilation errors. Run all existing tests to verify functionality is preserved after the upgrade.

**Done when**: Solution builds successfully and all tests pass (or test results are reviewed if no tests exist).