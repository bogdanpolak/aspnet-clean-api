# MediatR and FluentValidation with API Error Handling

## Setup - Packages

- MediatR
- MediatR.Extensions.Microsoft.DependencyInjection
- FluentValidation
- FluentValidation.DependencyInjectionExtensions

```ps
dotnet add package MediatR
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation
dotnet add package FluentValidation.DependencyInjectionExtensions
```

## Setup - Project

1. Copy folder `MediatorValidations`
2. Add registration code to `Setup.cs`
   ```
   services.AddValidationPipeline();
   services.AddMediatR_And_Validations(typeof(Startup).Assembly);
   ...
   app.UseMiddleware<ValidationErrorHandlingMiddleware>();
   ```
   
## Template Handler with Validation
 
```
public class Get{Something}Query : IRequest<Response>
{ 
}
    
public class Get{Something}QueryHandler : IRequestHandler<Get{Something}Query, Response>
{
   public Task<Response> Handle(Get{Something}Query request, CancellationToken cancellationToken)
   {
      throw new System.NotImplementedException();
   }
}

public class Get{Something}QueryValidator : AbstractValidator<Get{Something}Query>
{
   public Get{Something}QueryValidator() 
   {
      // RuleFor(x => x.FieldName).NotNull();
   }
}

public class Response 
{

}
```

> Rename `{Something}` => feature name (eg. ImagesSince)
