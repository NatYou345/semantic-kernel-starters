# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade sk-starters.sln projects from net8.0/netstandard2.0 to net10.0
**Scope**: 8 projects total — 6 require changes, 2 already on net10.0

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 8 projects with a flat dependency graph. Most upgrades are straightforward TFM/package bumps; Azure Functions V2 migration is the main complexity.

## Tasks

### 01-project-upgrades: Update target frameworks and NuGet packages

Update TargetFramework to net10.0 in all 5 project files needing TFM changes (ProductDocumentation, sk_csharp_apim_demo, sk-chatgpt-azure-function, sk-csharp-azure-functions, sk-typescript-console-chat). Update NuGet package references to .NET 10-compatible versions across all projects, including semantic-functions-generator (netstandard2.0, NuGet updates only). Replace the deprecated NuGet package in sk_csharp_apim_demo.

**Done when**: All project files have correct TargetFramework values and updated NuGet package references.

---

### 02-code-fixes: Fix breaking API changes and migrate Azure Functions

Address binary incompatible API calls in sk-chatgpt-azure-function and sk-csharp-azure-functions. Fix behavioral changes flagged in sk_csharp_apim_demo, sk-chatgpt-azure-function, and sk-csharp-azure-functions. Migrate the 2 Azure Functions projects from in-process to the isolated worker model (V2).

**Done when**: All binary-incompatible API calls replaced, behavioral changes addressed, and Azure Functions projects migrated to V2 isolated model.

---

### 03-build-validation: Build solution and resolve compilation errors

Build the complete solution and fix any remaining compilation errors in a single bounded pass.

**Done when**: Solution builds successfully with 0 errors.

---

### 04-test-validation: Run tests and verify functionality

Run all tests in the solution to verify the upgrade didn't break functionality.

**Done when**: All tests pass, or failures are documented with explanations.