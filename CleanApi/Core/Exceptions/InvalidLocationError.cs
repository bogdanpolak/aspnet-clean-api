using System;

namespace CleanApi.Core.Exceptions
{
    public class InvalidLocationError : CoreValidationError
    {
        private const string InvalidLocationMessage = 
            "{0} must be one of the following:" +
            " 'poland/cracow', 'india/chennai', 'usa/richfield'," +
            " 'usa/cleveland', 'usa/newyork'," +
            " 'usa/sanfranciso', 'usa/redmond'. You entered '{1}'";
        
        public InvalidLocationError(string propertyName, string actualLocation) : base(
            propertyName, string.Format(InvalidLocationMessage, propertyName, actualLocation))
        {
        }
    }
}