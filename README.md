# Recycli <img src="https://github.com/patrickmatula/Recycli/blob/main/Resources/delete-100px.svg" width="36">

Recycli is a tool to restore or delete files from the recycle bin.

![Recycli Application](https://github.com/patrickmatula/Recycli/blob/main/Resources/recycli_readme.png)

## Why does this tool exist?

This tool can be helpful in a situation where you delete a file with admin rights and it ended up in the Recycle Bin, but you can't easily restore it because explorer.exe is always running with user rights.

## Your code sucks!

That's very likely, because I'm not a professional developer. If you have any suggestions, improvements or bug reports, please [open an issue](https://github.com/patrickmatula/Recycli/issues) on GitHub.  

## Your design sucks!

That's very likely, because I'm not a professional designer. The goal is to use the Fluent Design, so it looks like a native Windows app. If you have any suggestions, improvements or bug reports, please [open an issue](https://github.com/patrickmatula/Recycli/issues) on GitHub.

## Logging

Recycli uses [Microsoft.Extensions.Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging) for logging.  
By default, logs are written to the debug output (viewable with DebugView from Sysinternals).

## Used patterns / packages

- MVVM pattern with [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)
- Dependency Injection with [Microsoft.Extensions.DependencyInjection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) and [Microsoft.Hosting](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host)
- Logging with [Microsoft.Extensions.Logging](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
- COM interop with [Interop.Shell32](https://learn.microsoft.com/de-de/windows/win32/shell/versions)

> [!NOTE]  
> Some parts of this project were developed with the assistance of GitHub Copilot.