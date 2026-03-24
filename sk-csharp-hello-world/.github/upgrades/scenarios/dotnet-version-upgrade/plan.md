# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade sk-csharp-hello-world from net8.0 to net10.0
**Scope**: 1 project, ~239 LOC, straightforward TFM bump with 3 package upgrades and 2 binary-incompatible API fixes

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 1 project, currently on net8.0, low difficulty, straightforward TFM/package bumps with minor API fixes.

## Tasks

### 01-upgrade-project: Upgrade sk-csharp-hello-world to net10.0

Update the project's target framework from net8.0 to net10.0, upgrade all NuGet packages to their recommended versions, and fix the 2 binary-incompatible API issues with `ConfigurationBinder.Get<T>`.

Packages to upgrade:
- Microsoft.Extensions.Configuration.UserSecrets 8.0.0 → 10.0.5
- Microsoft.Extensions.Logging.Console 6.0.0 → 10.0.5
- Microsoft.Extensions.Logging.Debug 6.0.0 → 10.0.5

API fixes needed:
- `ConfigurationBinder.Get<T>` — 2 occurrences, binary incompatible in net10.0

**Done when**: Project targets net10.0, all packages updated, solution builds with 0 errors.

---

### 02-run-tests: Validate tests pass

Run all tests in the solution and verify they pass after the upgrade.

**Done when**: All tests pass or test failures are triaged and documented.
