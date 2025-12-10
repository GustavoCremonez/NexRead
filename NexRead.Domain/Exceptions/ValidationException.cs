namespace NexRead.Domain.Exceptions;

public class ValidationException : DomainException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("Ocorreram um ou mais erros de validação.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Ocorreram um ou mais erros de validação.")
    {
        Errors = errors;
    }

    public ValidationException(string propertyName, string errorMessage)
        : base("Ocorreram um ou mais erros de validação.")
    {
        Errors = new Dictionary<string, string[]>
        {
            { propertyName, new[] { errorMessage } }
        };
    }
}
