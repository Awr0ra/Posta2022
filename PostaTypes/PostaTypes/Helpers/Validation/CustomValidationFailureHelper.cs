using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PostaTypes.Contracts.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace PostaTypes.Helpers.Validation;

public static class CustomValidationFailureHelper
{
    private const string ERROR_CODE = "validation_error";
    private const string PRIORITY = "MEDIUM";
    
    public static CustomValidationFailureResponse ErrorDetail { get; set; } = new CustomValidationFailureResponse();
    
    public static IActionResult MakeValidationResponse(ActionContext context)
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
        };
        foreach (var keyModelStatePair in context.ModelState)
        {
            var errors = keyModelStatePair.Value.Errors;
            if (errors != null && errors.Count > 0)
            {
                if (errors.Count == 1)
                {
                    var errorMessage = GetErrorMessage(errors[0]);
                    ErrorDetail.errors.Add(new Error()
                    {
                        priority = PRIORITY,
                        message = errorMessage,
                        errorCode = ERROR_CODE,
                        properties = new Properties()
                        {
                            name = "string",
                            value = "string"
                        }
                    });
                }
                else
                {
                    var errorMessages = new string[errors.Count];
                    for (var i = 0; i < errors.Count; i++)
                    {
                        errorMessages[i] = GetErrorMessage(errors[i]);
                        ErrorDetail.errors.Add(new Error()
                        {
                            message = errorMessages[i],
                            errorCode = ERROR_CODE,
                            priority = PRIORITY
                        });
                    }
                }
            }
        }
        ErrorDetail.traceId = context.HttpContext.TraceIdentifier;
        ErrorDetail.timestamp = DateTime.Now;

        var result = new BadRequestObjectResult(ErrorDetail);

        result.ContentTypes.Add("application/problem+json");

        return result;
    }

    static string GetErrorMessage(ModelError error)
    {
        return string.IsNullOrEmpty(error.ErrorMessage) ?
        "The input was not valid." :
        error.ErrorMessage;
    }
}