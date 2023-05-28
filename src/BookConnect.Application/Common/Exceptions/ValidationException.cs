using FluentValidation.Results;

namespace BookConnect.Application.Common.Exceptions;

public class ValidationException : Exception
{
     public readonly IDictionary<string, string[]> Errors;
    public ValidationException(): base("One or more validation error has occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToArray());
    }
}
