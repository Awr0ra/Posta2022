using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PostaTypes.Contracts.Requests.Validation;

namespace Posta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
        [HttpPost]
        [Route("valid-request")]
        public async Task<IActionResult> ValidationCheck(
                            ValidationCheckRequest request,
                            [FromServices] IValidator<ValidationCheckRequest> validator)
        {
            // 1st variant
            //await validator.ValidateAndThrowAsync(request);

            // 2nd variant
            /**/
            var validated = validator.Validate(request);
            if (!validated.IsValid)
            { 
                var modelStateDictionary = new ModelStateDictionary();
                foreach (ValidationFailure item in validated.Errors)
                    modelStateDictionary.AddModelError(item.PropertyName, item.ErrorMessage);

                return ValidationProblem(modelStateDictionary);
            }
            /**/

            return Ok();
        }
    }

}
