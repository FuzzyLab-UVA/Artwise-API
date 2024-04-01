namespace ArtwiseAPI.Common;

/// <summary>
/// Defines HTTP media types.
/// </summary>
internal static class MediaType
{
    /// <summary>
    /// Defines a response/request in plain JSON text format.
    /// </summary>
    public const string AppJson = "application/json";

    /// <summary>
    /// Defines a response/request in plain JSON text format (old type, still sometimes used for historical reasons).
    /// </summary>
    public const string TextJson = "text/json";

    /// <summary>
    /// Defines a response that did not complete successfully.
    /// </summary>
    public const string ProblemJson = "application/problem+json";
}