using System.Text.RegularExpressions;

namespace ArtwiseAPI.Common;

/// <summary>
/// Defines the API's global <see langword="static"/> properties. 
/// </summary>
public sealed partial class ApiStatics
{
    /// <summary>
    /// Regex to match a valid e-mail address.
    /// </summary>
    public static Regex EmailRegex { get; } = GenerateEmailRegex();

    [GeneratedRegex(@"(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$)", RegexOptions.Compiled)]
    private static partial Regex GenerateEmailRegex();
}