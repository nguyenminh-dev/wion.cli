You are a senior .NET developer.

Your task is to build a CLI tool named `wion` that generates a new ABP-based project from an existing template source code.

## Template Information

The template project structure is:

* Root folder: `Wion.Template`

* Solution file: `Wion.Template.sln`

* ABP module structure inside `src/`:

  * Wion.Template.Domain
  * Wion.Template.Domain.Shared
  * Wion.Template.Application
  * Wion.Template.Application.Contracts
  * Wion.Template.EntityFrameworkCore
  * Wion.Template.HttpApi
  * Wion.Template.HttpApi.Client
  * Wion.Template.HttpApi.Host
  * Wion.Template.DbMigrator

* Test projects inside `test/`:

  * Wion.Template.Application.Tests
  * Wion.Template.Domain.Tests
  * Wion.Template.EntityFrameworkCore.Tests
  * Wion.Template.HttpApi.Client.ConsoleTestApp
  * Wion.Template.TestBase

* Other files:

  * Wion.Template.abpmdl
  * Wion.Template.abpsln
  * common.props
  * NuGet.Config

## CLI Requirements

Command:

wion new <ProjectName>

Example:

wion new Wion.Invoice

## Expected Behavior

1. Copy entire template directory

2. Rename ALL occurrences of:

   * "Wion.Template" → "<ProjectName>"

This includes:

* Folder names
* File names
* File contents
* Namespaces
* Solution name

3. Ignore folders:

* bin/
* obj/
* .vs/

4. Must support:

* Recursive directory processing
* UTF-8 encoding
* Logging processed files

## Technical Constraints

* .NET 6.0 Console Application
* ABP Framework 5.2.2
* Use clean architecture
* Separate responsibilities:

  * TemplateProcessor
  * FileReplacer
  * DirectoryRenamer

## Framework Requirements

* Target Framework: net6.0
* ABP Framework: 5.2.2
* Do NOT use .NET 9.0 - all projects must target net6.0

## Output Requirements

Return:

1. Full source code of CLI
2. Folder structure of CLI
3. Key implementation explanation

IMPORTANT:

* Do NOT skip file renaming logic
* Do NOT use pseudo code
* Provide real working code
* Ensure ABP solution still builds after generation
