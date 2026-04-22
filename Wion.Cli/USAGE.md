# Wion CLI - Usage Guide

## Quick Start

### 1. Build the CLI

```powershell
# Using PowerShell
.\build.ps1 Build

# Or using batch
.\build.bat Build
```

### 2. Publish to PATH

```powershell
# Using PowerShell
.\build.ps1 Publish

# Or using batch
.\build.bat Publish
```

### 3. Add to PATH

Add the published output to your system PATH:

**Windows:**
1. Open **System Properties → Advanced → Environment Variables**
2. Under **System Variables**, find `Path`
3. Click **Edit → New**
4. Add: `C:\Users\minhnv\Desktop\WionCli\Wion.Cli\bin\Release\net6.0\publish`

### 4. Create a New Project

```powershell
# Navigate to where you want to create the project
cd C:\Projects

# Create a new project
wion new Wion.Invoice

# Or with verbose logging
wion new Wion.Invoice --verbose
```

## Command Reference

### `wion new <ProjectName>`

Creates a new ABP-based project from the Wion.Template template.

**Arguments:**
- `projectName` (required): The name of the project to create

**Options:**
- `--verbose`: Enable verbose logging to see detailed processing information

**Example:**
```powershell
wion new Wion.Invoice --verbose
```

## What Gets Generated

When you run `wion new Wion.Invoice`, the CLI will:

1. Copy the entire `Wion.Template` directory
2. Replace all occurrences of "Wion.Template" with "Wion.Invoice"
3. Rename all folders containing "Wion.Template"
4. Rename all files containing "Wion.Template"
5. Update all file contents (namespaces, references, constants, etc.)

The result is a fully functional ABP-based project ready for development.

## Output Structure

```
Wion.Invoice/
├── src/
│   ├── Wion.Invoice.Domain/
│   ├── Wion.Invoice.Domain.Shared/
│   ├── Wion.Invoice.Application/
│   ├── Wion.Invoice.Application.Contracts/
│   ├── Wion.Invoice.EntityFrameworkCore/
│   ├── Wion.Invoice.HttpApi/
│   ├── Wion.Invoice.HttpApi.Client/
│   ├── Wion.Invoice.HttpApi.Host/
│   └── Wion.Invoice.DbMigrator/
├── test/
│   ├── Wion.Invoice.Application.Tests/
│   ├── Wion.Invoice.Domain.Tests/
│   ├── Wion.Invoice.EntityFrameworkCore.Tests/
│   ├── Wion.Invoice.HttpApi.Client.ConsoleTestApp/
│   └── Wion.Invoice.TestBase/
├── Wion.Invoice.sln
├── Wion.Invoice.abpmdl
├── Wion.Invoice.abpsln
├── common.props
└── NuGet.Config
```

## Next Steps

After creating a new project:

```powershell
cd Wion.Invoice
dotnet restore
dotnet build
```

## Troubleshooting

### "Template directory not found"

If you see this error:
1. Make sure you're running the CLI from the correct location
2. Check that the `Wion.Template` directory exists next to the CLI
3. Try running from the solution directory

### "Output directory already exists"

The CLI won't overwrite existing directories. Either:
1. Delete the existing directory, or
2. Choose a different project name

### Build Errors

If the generated project doesn't build:
1. Make sure all NuGet packages are restored: `dotnet restore`
2. Check that the .NET 6 SDK is installed
3. Verify that no template references were missed (file an issue if so)