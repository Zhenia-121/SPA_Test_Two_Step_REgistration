using FluentValidation.Results;
using System.Linq;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
        FlatErrors = new List<string>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(List<string> errors)
        : this()
    {
        FlatErrors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }

    public List<string> FlatErrors { get; set; }

    public override string ToString()
    {
        return string.Join(", ", Errors.Select(e => $"{e.Key} : { string.Join(' ', e.Value)}").Concat(FlatErrors));
    }
}
