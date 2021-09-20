using FluentValidation;

namespace CleanApi.Core
{
    public class GetForecastQueryValidator : AbstractValidator<GetForecastQuery.Request>
    {
        public GetForecastQueryValidator()
        {
            RuleFor(x => x.Location)
                .NotEmpty()
                .Matches(@"\w+\/\w+")
                .WithMessage("{PropertyName} has incorrect format, expected: country/city");
            RuleFor(x => x.Days)
                .InclusiveBetween(1, 14);
        }
    }
}