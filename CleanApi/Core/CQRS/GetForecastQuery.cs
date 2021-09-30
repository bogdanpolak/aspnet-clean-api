using CleanApi.Core.Models;
using MediatR;

namespace CleanApi.Core.CQRS
{
    public class GetForecastQuery : IRequest<Forecast> 
    {
        public string Location;
        public int? Days;

        public GetForecastQuery(string location, int? days)
        {
            Location = location;
            Days = days;
        }
    }
}