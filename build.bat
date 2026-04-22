@echo off
setlocal enabledelayedexpansion

if "%1"=="" set TASK=Build
if "%1"=="Build" set TASK=Build
if "%1"=="Publish" set TASK=Publish
if "%1"=="Clean" set TASK=Clean
if "%1"=="Test" set TASK=Test

if "%2"=="" set CONFIG=Release
set CONFIG=%2

echo.
echo Running: !TASK!
echo.

if "!TASK!"=="Build" (
    dotnet build Wion.Cli\Wion.Cli.csproj -c !CONFIG!
) else if "!TASK!"=="Publish" (
    dotnet publish Wion.Cli\Wion.Cli.csproj -c !CONFIG! -o Wion.Cli\bin\!CONFIG!\net6.0\publish
    echo.
    echo To use the CLI, add the following path to your PATH:
    echo %CD%\Wion.Cli\bin\!CONFIG!\net6.0\publish
    echo.
    echo Then run: wion new ^<ProjectName^>
    echo.
) else if "!TASK!"=="Clean" (
    dotnet clean Wion.Cli\Wion.Cli.csproj -c !CONFIG!
) else if "!TASK!"=="Test" (
    dotnet test Wion.Cli\Wion.Cli.csproj -c !CONFIG!
)

if !ERRORLEVEL! NEQ 0 (
    echo.
    echo Error: Command failed!
    exit /b 1
)

echo.
echo Success!
