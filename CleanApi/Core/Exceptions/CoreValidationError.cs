namespace CleanApi.Core.Exceptions
{
    public class CoreValidationError : CoreException
    {
        public string PropertyName { get; }

        public CoreValidationError(string propertyName, string errorMessage) : base(errorMessage)
        {
            PropertyName = propertyName;
        }
        
    }
}