using System.CommandLine;
using Wion.Cli.Commands;

var rootCommand = new RootCommand("Wion CLI - Generate new ABP-based projects from template");
NewCommand.Configure(rootCommand);

return await rootCommand.InvokeAsync(args);