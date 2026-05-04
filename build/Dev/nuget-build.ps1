param (
    [string]$version = "1.0.0"
)

$currentFolder = $PSScriptRoot
$rootFolder = Join-Path $currentFolder "../../"
$cliProject = Join-Path $rootFolder "Wion.Cli/Wion.Cli.csproj"
$nugetHost = "https://package.public.rke.app.dev.tmtco.org"
$nugetKey = ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Wion.Cli NuGet Build & Push Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Version: $version" -ForegroundColor Yellow
Write-Host "NuGet Host: $nugetHost" -ForegroundColor Yellow
Write-Host ""

# Validate paths
if (-not (Test-Path $cliProject)) {
    Write-Host "ERROR: CLI project not found at: $cliProject" -ForegroundColor Red
    exit 1
}

# Step 1: Clean previous builds
Write-Host "[1/4] Cleaning previous builds..." -ForegroundColor Green
dotnet clean $cliProject --configuration Release

# Step 2: Build the project
Write-Host "[2/4] Building Wion.Cli..." -ForegroundColor Green
dotnet build $cliProject --configuration Release /p:Version=$version

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    exit 1
}

# Step 3: Pack the NuGet package
Write-Host "[3/4] Creating NuGet package..." -ForegroundColor Green
dotnet pack $cliProject --configuration Release /p:Version=$version --no-build

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Pack failed!" -ForegroundColor Red
    exit 1
}

# Step 4: Find and push the package
Write-Host "[4/4] Pushing to NuGet feed..." -ForegroundColor Green
$nupkgFile = Get-ChildItem -Path (Join-Path $rootFolder "Wion.Cli/bin/Release") -Filter "*.nupkg" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($nupkgFile) {
    Write-Host "Found package: $($nupkgFile.Name)" -ForegroundColor Cyan

    # Confirm before pushing
    Write-Host ""
    Write-Host "Ready to push $($nupkgFile.Name) to $nugetHost" -ForegroundColor Yellow
    $confirm = Read-Host "Continue? (y/N)"

    if ($confirm -eq "y" -or $confirm -eq "Y") {
        dotnet nuget push $nupkgFile.FullName --source $nugetHost --api-key $nugetKey --skip-duplicate

        if ($LASTEXITCODE -eq 0) {
            Write-Host ""
            Write-Host "========================================" -ForegroundColor Green
            Write-Host "SUCCESS! Package pushed successfully!" -ForegroundColor Green
            Write-Host "========================================" -ForegroundColor Green
            Write-Host ""
            Write-Host "Install with: dotnet tool install --global Wion.Cli --version $version --add-source $nugetHost" -ForegroundColor Cyan
        } else {
            Write-Host "ERROR: Push failed!" -ForegroundColor Red
            exit 1
        }
    } else {
        Write-Host "Push cancelled." -ForegroundColor Yellow
    }
} else {
    Write-Host "ERROR: No .nupkg file found!" -ForegroundColor Red
    exit 1
}