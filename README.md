Repro where a `ILoggerProvider` registered as single gets instantiated twice.

```sh

$ dotnet run
Using launch settings from Properties\launchSettings.json...
Creating instance. Total: 1
Creating instance. Total: 2
Hosting environment: Development
Content root path: ...
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
Application is shutting down...
^C
Dispose called. Total: 1
Dispose called. Total: 2
```