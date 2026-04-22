param(
    [Parameter(Position = 0)]
    [ValidateSet("Build", "Publish", "Clean", "Test")]
    [string]$Task = "Build",

    [Parameter(Position = 1)]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

function Write-Step {
    param([string]$Message)
    Write-Host "`n$Message" -ForegroundColor Cyan
}

function Write-Success {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Green
}

function Write-Error {
    param([string]$Message)
    Write-Host $Message -ForegroundColor Red
}

$SolutionRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectFile = Join-Path $SolutionRoot "Wion.Cli\Wion.Cli.csproj"

switch ($Task) {
    "Build" {
        Write-Step "Building Wion.Cli..."
        dotnet build $ProjectFile -c $Configuration
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Build successful!"
        } else {
            Write-Error "Build failed!"
            exit 1
        }
    }

    "Publish" {
        Write-Step "Publishing Wion.Cli..."
        $PublishPath = Join-Path $SolutionRoot "Wion.Cli\bin\$Configuration\net6.0\publish"
        dotnet publish $ProjectFile -c $Configuration -o $PublishPath
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Publish successful!"
            Write-Host "`nTo use the CLI, add the following path to your PATH:" -ForegroundColor Yellow
            Write-Host $PublishPath -ForegroundColor White
            Write-Host "`nThen run: wion new <ProjectName>`n"
        } else {
            Write-Error "Publish failed!"
            exit 1
        }
    }

    "Clean" {
        Write-Step "Cleaning Wion.Cli..."
        dotnet clean $ProjectFile -c $Configuration
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Clean successful!"
        } else {
            Write-Error "Clean failed!"
            exit 1
        }
    }

    "Test" {
        Write-Step "Testing Wion.Cli..."
        dotnet test $ProjectFile -c $Configuration
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Tests passed!"
        } else {
            Write-Error "Tests failed!"
            exit 1
        }
    }
}