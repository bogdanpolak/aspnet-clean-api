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
public class Get{Something}Query
{
    public class Request : IRequest<Response> { }
    
    public class Handler : IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

 public class Get{Something}QueryValidator : AbstractValidator<Get{Something}Query.Request>
 {
     public Validator() 
     {
         // RuleFor(request => request.FieldName).
     }
 }

public class Response 
{

}
```
