# Repository Guidelines

This guide helps contributors work effectively in the PDFPass (.NET/C#) codebase. It summarizes layout, commands, and
conventions used here.

## Project Structure & Module Organization

- `PDFPass.sln`: Solution entry point.
- `src/PDFPass/PDFPass.csproj`: Main WinForms project (`net10.0-windows`), AnyCPU with `win-x64` publishing.
- `src/PDFPass/Program.cs`: Application entry point (`PDFPass`) and `src/PDFPass/lock.ico`.
- `src/PDFPass/Main/`, `Input/`, `Preferences/`, `Pdf/`, `Controls/`, `Localization/`: Feature/responsibility folders with matching `PDFPass.<Feature>` namespaces.
- `src/PDFPass/Properties/` and `src/PDFPass/Resources/`: Assembly/project metadata and localized resources. Keep `PDFPass.Properties` and `PDFPass.Resources` identities unchanged; `AssemblyInfo.cs` remains namespace-free with `GenerateAssemblyInfo=false`.
- `cmds/`, `pdfpasss-inno.iss`, `others/`: Portable helper scripts, Inno Setup script, and bundled release materials.
- `bin/`, `obj/`, `out/`: Build outputs (ignored by Git).

## Build, Test, and Development Commands

- SDK: install the exact stable .NET 10 SDK in root `global.json` (currently `10.0.404`); roll-forward and previews are disabled.
- Restore: `dotnet restore PDFPass.sln --locked-mode`
- Build: `dotnet build PDFPass.sln -c Debug` or `dotnet build PDFPass.sln -c Release`
- Run (GUI): `dotnet run --project src/PDFPass/PDFPass.csproj`
- Publish restore (CI-aligned): `dotnet restore src/PDFPass/PDFPass.csproj -r win-x64 --locked-mode -p:SelfContained=true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true`
- Publish after that restore: `dotnet publish src/PDFPass/PDFPass.csproj -r win-x64 -c Release -p:DebugType=none --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --no-restore -o "bin\Release"`
- Publish (framework-dependent local output): `dotnet publish src/PDFPass/PDFPass.csproj -c Release -r win-x64 --self-contained false -o out`
- Tests: No test project or test/coverage packages are currently present.

## Coding Style & Naming Conventions

- SDK-default C# language version, 4-space indent, file-scoped namespaces, nullable enabled.
- Organize application code under `src/PDFPass/<Feature>` with matching `PDFPass.<Feature>` namespaces, except `Properties` and `Resources` resource identities and namespace-free `Properties/AssemblyInfo.cs`.
- Names: types/properties/methods use PascalCase; fields/local variables camelCase; constants UPPER_SNAKE_CASE.
- Async methods end with `Async`. Prefer `readonly`/`record` where appropriate.
- Analyzers: .NET analyzers are enabled (`EnableNETAnalyzers=true`, `AnalysisLevel=latest`); `TreatWarningsAsErrors` is currently `false` in `src/PDFPass/PDFPass.csproj`. Format with `dotnet format` when explicitly formatting.

## Testing Guidelines

- Future convention: place unit tests in `tests/PDFPass.Tests` (for example with xUnit and `coverlet.collector`) and add the project to `PDFPass.sln`.
- Once added, run tests from repo root with `dotnet test PDFPass.sln -c Release`.
- Optional coverage once configured:
  `dotnet test -c Release --collect:"XPlat Code Coverage"`.
- Reports: use `reportgenerator` (optional) to convert to HTML.

## Commit & Pull Request Guidelines

- Conventional Commits: `feat:`, `fix:`, `docs:`, `refactor:`, `test:`, `chore:`.
- Messages are imperative and reference issues (e.g., `#123`).
- PRs include: concise summary, rationale, test notes/coverage, and run instructions; add screenshots/logs for behavior
  changes.

## Security & Configuration Tips

- Never commit secrets or proprietary PDFs. Use `dotnet user-secrets` (dev) or environment variables (prod).
- Store runtime config in `appsettings*.json`; do not commit machine-specific files.
- Validate input paths and restrict file I/O to safe directories by default.
- Maintainer prerequisite: revoke/rotate any PAT previously embedded in a local remote URL, replace it with credential-free HTTPS using an approved credential manager, and restrict/redact retained output. Never print the credential. CI publication uses only the short-lived `GITHUB_TOKEN`.

## Release Maintenance

- The application workflow runs only on pushes to `master`, never on pull requests (including fork PRs). The build job runs full publish, ZIP byte/hash validation and Inno installer validation with read-only repository permissions. Final `release-assets` artifacts are retained for 14 days; package validation is not application unit testing.
- Only successful `master` pushes publish, deriving the version exclusively from `src/PDFPass/Properties/AssemblyInfo.cs`. Published versions are skipped without changing tags/assets. Drafts and wrong-commit tags fail closed and need maintainer review; never clobber assets, move tags, or blindly resume a draft.
- The publisher creates a draft, verifies commit association and assets/checksums, then publishes as latest. Checksums establish integrity, not publisher identity; existing manifest settings do not prove Authenticode signing.
- Keep `packages.lock.json` committed. The project declares `RuntimeIdentifiers` (not a fixed `RuntimeIdentifier`) and `PublishSingleFile` so ordinary and publish restores use a stable dependency graph. Regenerate intentionally with CI restore properties and `--force-evaluate` when updating dependencies, then verify locked cold/warm restore.
- Dependabot proposes weekly grouped Actions and NuGet updates without auto-merge. Review SDK and Inno Setup pins explicitly; a pinned runner family is not an immutable image.
- Master runs do not cancel in progress. Branch-scoped concurrency can replace pending runs and is not a durable release queue. Bump versions in discrete completed master runs.
- Rollout: merge migration/hardening without changing the existing `2026.13.0` version, which must skip its existing release. Validate that master run's artifact manually on clean Windows VMs, then make a separate version bump. New-release notes must mention the .NET 10/x64 migration.
- New packages are x64-only and self-contained. Windows 7/8 are unsupported; old x86 downloads are historical, not maintained fallbacks. Test supported Windows 11 and serviced Windows 10 editions, not arbitrary legacy installations; native ARM64 is out of scope.
- Before the first release bump, test portable/installed launch without a separate runtime, PDF encryption/decryption/watermarking, drag/drop, password paste, language/settings persistence, Unicode/spaced paths, shortcuts/context menu, upgrade and uninstall. Preserve HKCU preferences/owner password. Run registration/uninstall scripts only in disposable VMs, never on the developer's main machine.
- Maintainers should review rulesets/branch protection for compatibility with the master-only workflow; do not require a PR build check that will never run. Remove unnecessary default token write permissions. Do not impose a new approval policy without authorization.
