# Recycli ![recycli fluent icon](...)

Recycli is a tool to restore or delete files from the recycle bin.

## Why does this tool exist?

This tool can be helpful in a situation where you deleted a file with admin rights and it ended up in the recycle bin, but you can't easily restore it because explorer.exe is always running with user rights.

## Your code sucks!

That's very likely, because I'm not a professional developer. If you have any suggestions, improvements or bug reports, please open an issue on GitHub.  
You can also participate in the the [Code Review Stack Exchange](...).

## Your design sucks!

The goal is to use the fluent design, so it looks like a native Windows app. If you have any suggestions, improvements or bug reports, please open an issue on GitHub.

## Logging

Recycli uses [Microsoft.Extensions.Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging) for logging.  
By default, logs are written to the debug output (viewable with DebugView from Sysinternals).

> [!NOTE]  
> Some parts of this project were developed with the assistance of GitHub Copilot. (Use as a Stackoverflow alternative but not as a vibe coding tool)