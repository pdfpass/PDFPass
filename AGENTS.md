# Repository Guidelines

This guide helps contributors work effectively in the PDFPass (.NET/C#) codebase. It summarizes layout, commands, and conventions used here.

## Project Structure & Module Organization
- `PDFPass.sln`: Solution entry point.
- `PDFPass.csproj`: Main project (WinForms, `net8.0-windows`).
- `Program.cs`, `Frm*.cs`, `*.Designer.cs`, `Properties/`: Application code and forms.
- `assets/`: Sample PDFs and fixtures (keep small, anonymized).
- `scripts/`: Helper scripts for local tasks (optional).
- `bin/`, `obj/`, `out/`: Build outputs (ignored by Git).

## Build, Test, and Development Commands
- Restore: `dotnet restore`
- Build: `dotnet build -c Release`
- Run (GUI): `dotnet run` (or `dotnet run --project PDFPass.csproj`)
- Publish: `dotnet publish -c Release -r win-x64 --self-contained false -o out`
- Tests: xUnit referenced, but no test project found. See Testing section to add one.

## Coding Style & Naming Conventions
- C# 12, 4-space indent, file-scoped namespaces, nullable enabled.
- Names: types/properties/methods use PascalCase; fields/local variables camelCase; constants UPPER_SNAKE_CASE.
- Async methods end with `Async`. Prefer `readonly`/`record` where appropriate.
- Analyzers: enable .NET analyzers; treat warnings as errors. Format with `dotnet format`.

## Testing Guidelines
- Framework: xUnit (project `tests/PDFPass.Tests`).
- Run tests: `dotnet test -c Release` (from repo root).
- Coverage: enabled via `coverlet.collector`. Generate coverage: `dotnet test -c Release --collect:"XPlat Code Coverage"`.
- Reports: use `reportgenerator` (optional) to convert to HTML.

## Commit & Pull Request Guidelines
- Conventional Commits: `feat:`, `fix:`, `docs:`, `refactor:`, `test:`, `chore:`.
- Messages are imperative and reference issues (e.g., `#123`).
- PRs include: concise summary, rationale, test notes/coverage, and run instructions; add screenshots/logs for behavior changes.

## Security & Configuration Tips
- Never commit secrets or proprietary PDFs. Use `dotnet user-secrets` (dev) or environment variables (prod).
- Store runtime config in `appsettings*.json`; do not commit machine-specific files.
- Validate input paths and restrict file I/O to safe directories by default.
