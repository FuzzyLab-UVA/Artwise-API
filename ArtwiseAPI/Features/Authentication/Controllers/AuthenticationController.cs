using ArtwiseAPI.Common;
using ArtwiseAPI.Features.Authentication.Models;
using ArtwiseAPI.Features.Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ArtwiseAPI.Features.Authentication.Controllers;

/// <summary>
/// Controller for user authentication.
/// </summary>
[ApiController, Route(ApiConstants.MainEndpoint)]
[Consumes(MediaType.AppJson, MediaType.TextJson)]
public sealed class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _service;

    /// <summary>
    /// Controller for user authentication.
    /// </summary>
    /// <param name="service">The service that handles user authentication.</param>
    public AuthenticationController(AuthenticationService service)
        => _service = service;

    /// <summary>
    /// Authenticates a user and returns a session token to them.
    /// </summary>
    /// <param name="request">The authentication request.</param>
    /// <returns>A <see cref="AuthenticationResponse"/> or a <see cref="ProblemHttpResult"/>.</returns>
    [HttpPost, AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<Results<Ok<AuthenticationResponse>, ProblemHttpResult>> AuthenticateAsync([FromBody] AuthenticationRequest request)
    {
        try
        {
            return TypedResults.Ok(await _service.LoginAsync(request));
        }
        catch (InvalidOperationException ex)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: ex.Message
            );
        }
    }
}