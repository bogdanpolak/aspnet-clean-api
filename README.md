# Clean API Template

ASP.NET Core API using MediatR and FluentValidation to separate concerns with middleware for ValidationException error handling. Project contains template and sample ASP.NET Core application which is refactored classic ASP.NET Core API template (Weather Forecast App).

> **Important notes** <br> 
> * `Core` subfolder should be moved into separate `Core.csproj` class library
> * `Infrastructure` subfolder should be moved into `Infrastructure.csproj`
> More info about Clean Architecture: https://jasontaylor.dev/clean-architecture-getting-started/

## New project setup

1. Add NuGet packages
    ```
    dotnet add package MediatR
    dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
    dotnet add package FluentValidation
    dotnet add package FluentValidation.DependencyInjectionExtensions
    ```
2. Copy/Create units:
   * `ValidationPipelineBehavior.cs` - [link](CleanApi/Core/Behaviors/ValidationPipelineBehavior.cs)
   * `ValidationErrorHandlingMiddleware.cs` - [link](CleanApi/ValidationErrorHandlingMiddleware.cs)
   

3. Update `Startup.cs`:
    ```c#
    // Register method:
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    var assembly = typeof(Startup).Assembly;
    services.AddMediatR(assembly);
    services.AddValidatorsFromAssembly(assembly);
    // Configure method:
    app.UseMiddleware<ValidationErrorHandlingMiddleware>();
    ```
4. Add Mediator **Request** and **Handler** (rename `Forecast` to your domain subject)
    ```c#
    public class GetForecastQuery : IRequest<Forecast> { ... }
    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, Forecast> { ... }
    ```
5. Add Fluent Validator
    ```c#
    public class GetForecastQueryValidator : AbstractValidator<GetForecastQuery> { ... }
    ```
