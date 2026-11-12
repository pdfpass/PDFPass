# Repository Guidelines

This guide helps contributors work effectively in the PDFPass (.NET/C#) codebase. It summarizes layout, commands, and
conventions used here.

## Project Structure & Module Organization

- `PDFPass.sln`: Solution entry point.
- `src/PDFPass/PDFPass.csproj`: Main WinForms project (`net8.0-windows7.0`).
- `src/PDFPass/Program.cs`: Application entry point (`PDFPass`) and `src/PDFPass/lock.ico`.
- `src/PDFPass/Main/`, `Input/`, `Preferences/`, `Pdf/`, `Controls/`, `Localization/`: Feature/responsibility folders with matching `PDFPass.<Feature>` namespaces.
- `src/PDFPass/Properties/` and `src/PDFPass/Resources/`: Assembly/project metadata and localized resources. Keep `PDFPass.Properties` and `PDFPass.Resources` identities unchanged; `AssemblyInfo.cs` remains namespace-free with `GenerateAssemblyInfo=false`.
- `cmds/`, `pdfpasss-inno.iss`, `others/`: Portable helper scripts, Inno Setup script, and bundled release materials.
- `bin/`, `obj/`, `out/`: Build outputs (ignored by Git).

## Build, Test, and Development Commands

- Restore: `dotnet restore PDFPass.sln`
- Build: `dotnet build PDFPass.sln -c Debug` or `dotnet build PDFPass.sln -c Release`
- Run (GUI): `dotnet run --project src/PDFPass/PDFPass.csproj`
- Publish (CI-aligned): `dotnet publish src/PDFPass/PDFPass.csproj -r win-x86 -c Release -p:DebugType=none --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true -o "bin\Release\"`
- Publish (framework-dependent local output): `dotnet publish src/PDFPass/PDFPass.csproj -c Release -r win-x64 --self-contained false -o out`
- Tests: No test project or test/coverage packages are currently present.

## Coding Style & Naming Conventions

- C# 12, 4-space indent, file-scoped namespaces, nullable enabled.
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
