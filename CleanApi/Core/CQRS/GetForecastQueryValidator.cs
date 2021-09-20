using FluentValidation;

namespace CleanApi.Core.CQRS
{
    public class GetForecastQueryValidator : AbstractValidator<GetForecastQuery>
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