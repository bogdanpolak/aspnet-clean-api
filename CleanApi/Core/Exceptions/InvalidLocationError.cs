using System;

namespace CleanApi.Core.Exceptions
{
    public class InvalidLocationError : CoreException
    {
        public string Location { get; }

        public InvalidLocationError(string location) : base(
            $"Invalid location: '{location}'. Provide one of the following locations:" +
            " 'poland/cracow', 'india/chennai', 'usa/richfield', 'usa/cleveland', 'usa/newyork'," +
            " 'usa/sanfranciso', 'usa/redmond'")
        {
            Location = location;
        }
    }
}