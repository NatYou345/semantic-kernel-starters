# .NET Version Upgrade

## Strategy
**Selected**: All-at-Once — All projects upgraded simultaneously in a single operation.
**Rationale**: 8 projects (6 needing changes), flat dependency graph, mostly straightforward TFM/package bumps with Azure Functions V2 migration as the main complexity.

### Execution Constraints
- Single atomic upgrade — all TFM, package, and code changes applied together
- Validate full solution build after upgrade with 0 errors
- Testing comes after the atomic upgrade completes successfully

## Preferences
- **Flow Mode**: Automatic
- **Commit Strategy**: Single Commit at End
- **Target Framework**: net10.0 (.NET 10.0 LTS)
- **Solution**: sk-starters.sln

## Source Control
- **Source branch**: main
- **Working branch**: upgrade-to-NET10-2

## Decisions
- All-at-Once strategy auto-selected: 8 projects, flat dependency graph, no phasing needed
- No global.json present — no prerequisite config changes required

## Custom Instructions
<!-- Task-specific overrides: "For {taskId}: {instruction}" -->