# Wion.Cli

A .NET CLI tool that generates new ABP Framework-based projects from a template.

Instructions for installing this package can be found at https://package.public.rke.app.dev.tmtco.org/v3/index.json

## Usage

Create a new ABP project:

```bash
wion new <ProjectName>
```

### Example

```bash
wion new Wion.Example
```

This creates a new ABP layered application with:
- Domain, Application, and EntityFrameworkCore layers
- HttpApi.Host for running the API
- DbMigrator for database migrations and seeding
- Test projects using xUnit, NSubstitute, and Shouldly

## After Creating a Project

```bash
cd <ProjectName>

# Install ABP client-side libraries
abp install-libs

# Run database migrator
dotnet run --project src/<ProjectName>.DbMigrator

# Run the API host
dotnet run --project src/<ProjectName>.HttpApi.Host
```

## Requirements

- .NET 6.0 SDK
- ABP CLI (for `abp install-libs`)

## Technology Stack

- .NET 6.0
- ABP Framework 5.2.2
- Entity Framework Core
- ASP.NET Core
- xUnit for testing
