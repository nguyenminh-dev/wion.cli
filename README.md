# Wion CLI

Wion CLI is a tool used to generate a new ABP-based project with a standardized structure.

## Installation

1. Build the project:

```powershell
dotnet build
```

2. Publish the project:

```powershell
dotnet publish -c Release
```

3. Add to PATH:

* Open **System Properties → Advanced → Environment Variables**
* Under **System Variables**, find `Path`
* Click **Edit → New**
* Add the following path:
  `[your_project_path]/bin/Release/net6.0/publish`

---

## Usage

Create a new project:

```powershell
wion new Wion.Invoice
```

This command will generate a new project with the following structure:

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
│   ├── Wion.Invoice.Web/
│   └── Wion.Invoice.DbMigrator/
├── Wion.Invoice.sln
├── common.props
└── NuGet.Config
```

---

## Project Structure

* **Domain**: Contains entities and core business logic
* **Domain.Shared**: Contains constants, enums, and shared definitions
* **Application**: Contains application services and business logic orchestration
* **Application.Contracts**: Contains interfaces and DTOs
* **EntityFrameworkCore**: Contains DbContext and database migrations
* **HttpApi**: Contains API controllers
* **HttpApi.Client**: Contains API client proxies
* **Web**: Contains UI (Razor Pages)
* **DbMigrator**: Contains database migration runner
