# CES - Route-planning accelerator

This accelerator is intended to be a great start for CES projects, while showcasing some of the functionality and best practices from [Netcompany Foundation for .NET](https://goto.netcompany.com/cases/GTE1579/NCDOTNET).

The accelerator contains:
- Domain model with locations and connections.
- Pathing algorithm.
- Code-first database model in an in-memory database.
- Single-Page-Application frontend to interact with the domain.
- Simple user-authentication.
- External API with token-authorization.

## Getting started

1. Open the `RoutePlanning.sln` solution in Visual Studio.
2. You will be prompted for credentials to `source.netcompany.com`. Use your NCDMZ credentials.
   - Note that if you select "Remember my password", it will be saved in *Windows Credential Manager*.
3. Start the `RoutePlanning.Client.Web` project. (**F5** is the default shortcut)
4. Once you have the solution running it will show further information to get you started. Good luck :)
