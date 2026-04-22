# Wion CLI - Project Structure

## Overview

Wion CLI is a command-line tool that generates new ABP-based projects from a template source code. It follows clean architecture principles with separation of concerns.

## Project Structure

```
WionCli/
├── Wion.Cli/                          # Main CLI application
│   ├── Commands/
│   │   └── NewCommand.cs              # "new" command handler
│   ├── Services/
│   │   ├── FileReplacer.cs           # Handles file content replacements
│   │   ├── DirectoryRenamer.cs       # Handles directory and file renaming
│   │   └── Logger.cs                 # Logging service
│   ├── TemplateProcessor.cs           # Main processing logic
│   ├── Program.cs                     # Entry point
│   └── Wion.Cli.csproj               # Project file
├── Wion.Template/                     # ABP template source
│   ├── src/                           # Source projects
│   │   ├── Wion.Template.Domain/
│   │   ├── Wion.Template.Domain.Shared/
│   │   ├── Wion.Template.Application/
│   │   ├── Wion.Template.Application.Contracts/
│   │   ├── Wion.Template.EntityFrameworkCore/
│   │   ├── Wion.Template.HttpApi/
│   │   ├── Wion.Template.HttpApi.Client/
│   │   ├── Wion.Template.HttpApi.Host/
│   │   └── Wion.Template.DbMigrator/
│   ├── test/                          # Test projects
│   ├── Wion.Template.sln
│   ├── Wion.Template.abpmdl
│   └── Wion.Template.abpsln
├── Wion.Cli.sln                       # Solution file
├── build.ps1                          # PowerShell build script
├── build.bat                          # Batch build script
└── global.json                        # .NET SDK version
```

## Key Components

### TemplateProcessor

The `TemplateProcessor` class orchestrates the entire project generation process:

1. **Directory Copy**: Recursively copies the template directory structure
2. **File Processing**: Replaces all occurrences of "Wion.Template" with the new project name
3. **Directory Renaming**: Renames directories that contain the template name
4. **File Renaming**: Renames files that contain the template name

### FileReplacer

The `FileReplacer` service handles content replacement within files:

- Skips binary files (.dll, .exe, .pdb, images, etc.)
- Uses UTF-8 encoding for all text files
- Handles various replacement patterns (namespaces, solution references, constants, etc.)

### DirectoryRenamer

The `DirectoryRenamer` service handles:

- Directory renaming (deepest first to avoid path conflicts)
- File renaming
- Collision detection

### Logger

The `Logger` service provides:

- Info logging
- Debug logging (verbose mode)
- Warning logging
- Error logging

## Usage

### Building

```powershell
.\build.ps1 Build          # Build the project
.\build.ps1 Publish        # Publish to publish folder
.\build.ps1 Clean          # Clean build outputs
```

### Running

After building and adding to PATH:

```powershell
wion new Wion.Invoice      # Create a new project
wion new Wion.Invoice --verbose  # Create with verbose logging
```

## Technical Details

### .NET Version

- Target Framework: .NET 6.0
- C# Language Version: Latest

### Dependencies

- System.CommandLine 2.0.0-beta4.22272.1

### Ignored Folders

The following folders are ignored during template processing:
- bin/
- obj/
- .vs/
- .idea/
- node_modules/
- .git/

### File Replacements

The CLI performs the following replacements:

1. "Wion.Template" → "<ProjectName>"
2. Solution file references
3. Namespace declarations
4. Project references
5. Constant definitions
6. Configuration class names

## Clean Architecture

The CLI follows clean architecture principles:

1. **Separation of Concerns**: Each component has a single, well-defined responsibility
2. **Dependency Injection**: Services are injected into the processor
3. **Interface Segregation**: Services implement specific interfaces
4. **Single Responsibility**: Each class handles one aspect of the process