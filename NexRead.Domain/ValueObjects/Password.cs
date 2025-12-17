using NexRead.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace NexRead.Domain.ValueObjects;

public sealed class Password
{
    private const int MinLength = 8;
    private const int MaxLength = 40;

    private static readonly Regex UppercaseRegex = new(@"[A-Z]", RegexOptions.Compiled);
    private static readonly Regex LowercaseRegex = new(@"[a-z]", RegexOptions.Compiled);
    private static readonly Regex DigitRegex = new(@"\d", RegexOptions.Compiled);
    private static readonly Regex SpecialCharRegex = new(@"[!@#$%^&*(),.?""':;{}|<>_\-+=\[\]\\\/`~]", RegexOptions.Compiled);

    public string Value { get; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ValidationException("Password cannot be empty");

        if (password.Length < MinLength)
            throw new ValidationException($"Password must be at least {MinLength} characters long");

        if (password.Length > MaxLength)
            throw new ValidationException($"Password must not exceed {MaxLength} characters");

        if (!UppercaseRegex.IsMatch(password))
            throw new ValidationException("Password must contain at least one uppercase letter");

        if (!LowercaseRegex.IsMatch(password))
            throw new ValidationException("Password must contain at least one lowercase letter");

        if (!DigitRegex.IsMatch(password))
            throw new ValidationException("Password must contain at least one digit");

        if (!SpecialCharRegex.IsMatch(password))
            throw new ValidationException("Password must contain at least one special character");

        return new Password(password);
    }

    public override string ToString() => Value;
}
