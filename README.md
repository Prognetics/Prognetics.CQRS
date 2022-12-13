
# Welcome to Prognetics CQRS!

We would like to share with the community our companies production verified CQRS + Mediator library.

## **How it works?**

Throught the Mediator object you are able to issue different kinds of handlers to be called.

First are commands:
```c#
void Send<TCommand>(TCommand command) where TCommand : ICommand;

Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
```
They allow you to run a logic that is supposed to modify the state of the application. They do not return a value.

Second are queries:
```c#
TResult Fetch<TQuery, TResult>(TQuery query) where TQuery :  IQuery<TResult>;

Task<TResult> FetchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
```
They are used to return values, and should not perform any state modification operations.


Third are events:
```c#
Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
```
While each query or command can only have a single synchronous and asynchronous handler, this is not the case for events. When an event is published, the mediator will run all the handlers that are registered to handle it.

## **Integration**
This library provides an easy-to-use extension for integration with Autofac. To register handler implementations, it requires a collection of assemblies to be scanned:

```c#
builder.RegisterProgenticsCQRSModule(Assembly.GetExecutingAssembly());
```

The library can also be integreted with Microsoft Dependency Injection. 

```c#
builder.AddProgneticsCQRS(Assembly.GetExecutingAssembly());
```

**However, unlike Autofac, it does not support the use of generic handlers, so these will be skipped if they are defined.**


The library can be used without dependency injection. To define the way of resolving handlers in your application, implement the `Prognetics.CQRS.IHandlerResolver` interface and pass the implementation to `Prognetics.CQRS.Mediator`. The mediator will then be ready to work.

## **Defining Queries, Commands and Events**

### **Query**
The query must implement the `IQuery<TResult>` interface, where TResult is the type of the expected result:

```c#
public record MyQuery(string Data) : IQuery<string>
```

 You can implement the query handler in two ways:

 - **Synchronously** by implementing the `IQueryHandler<TQuery, TResult>` interface

```c#
public class MyQueryHandler : IQueryHandler<MyQuery, string>
{
    public string Handle(MyQuery query)
    {
        // Process the query
    }
}
```

 - **Asynchronously** by implementing the `IAsyncQueryHandler<TQuery, TResult>` interface

```c#
public class MyQueryHandler : IAsyncQueryHandler<MyQuery, string>
{
    public async Task<string> Handle(MyQuery query)
    {
        // Process the query
        // await sth
    }
}
```

where `TQuery` is the type of your query and `TResult` is the type of the result specified in the `IQuery<TResult>` interface.


### **Command**
The command must implement the `ICommand` interface:

```c#
public record MyCommand(string Data) : ICommand
```

Command handler can be:
 - **Synchronous** by implementing the `ICommandHandler<TCommand>` interface

```c#
public class SumCommandHandler : ICommandHandler<MyCommand>
{
    public void Handle(MyCommand command)
    {
        // Process the command
    }
}
```

 - **Asynchronously** by implementing the `IAsyncCommandHandler<TCommand>` interface

```c#
public class SumCommandHandler : IAsyncCommandHandler<MyCommand>
{
    public async Task Handle(MyCommand command)
    {
        // Process the command
        // await sth
    }
}
```
where `TCommand` is the type of your command

**NOTE: You can define both an asynchronous and synchronous handler for the same query or command**

### **Event**
The event must implement the `IEvent` interface.

```c#
public record MyEvent(string Data) : IEvent
```

Event handler can must be defined by implementing the `IEventHandler<TEvent>` interface:
```c#
public class MyEventHandler : IEventHandler<MyEvent>
{
    public async Task Handle(MyEvent @event)
    {
        // Process the event
        // await sth
    }
}
```
 where `TEvent` is the type of your event.



## Running Tests

The tests project is based on xunit with visual studio runner. To run right-click the project file and select "Run Tests".

