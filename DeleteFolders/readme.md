# About

An example to perform a recursive removal of a folder.

- Makes use of custom events
- Cancellation which does not help here but is good for 
    - Creating a timeout
    - A project with buttons to stop if needed.

In regards to a time out, replace

```csharp
private static readonly CancellationTokenSource cancellationToken = new();
```

With the following where in this case if the operation goes to four seconds an exception is thrown.

```csharp
private CancellationTokenSource cancellationToken = new (TimeSpan.FromSeconds(4));
```

In the code performing the removal, check for [TaskCanceledException](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcanceledexception?view=net-8.0).