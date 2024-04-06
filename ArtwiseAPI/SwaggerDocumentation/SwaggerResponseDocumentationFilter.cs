using ArtwiseAPI.Common;
using Kotz.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArtwiseAPI.SwaggerDocumentation;

/// <summary>
/// Adjusts the media types of endpoint responses in Swagger's documentation.
/// </summary>
/// <remarks>Can't use [Produces(...)] attribute from MVC, see https://github.com/dotnet/aspnetcore/issues/39802</remarks>
internal sealed class SwaggerResponseDocumentationFilter : IOperationFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var operationResponse in operation.Responses)
        {
            if (operationResponse.Key.StartsWith('2'))
            {
                // Remove all media types that are not "application/json" or "text/json" from successful responses.
                var mediaTypesToRemove = operationResponse.Value.Content.Keys.Where(x => !x.EqualsAny(MediaType.AppJson, MediaType.TextJson));

                foreach (var toRemove in mediaTypesToRemove)
                    operationResponse.Value.Content.Remove(toRemove);
            }
            else if (operationResponse.Key[0].EqualsAny('4', '5'))
            {
                // Remove all media types from unsuccessful responses and add one for "application/problem+json".
                operationResponse.Value.Content.Clear();
                operationResponse.Value.Content.Add(MediaType.ProblemJson, new() { Schema = context.SchemaRepository.Schemas[nameof(ProblemDetails)] });
            }
        }
    }
}