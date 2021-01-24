using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserApp.Api.Entities;
using UserApp.Api.Validators;
using System.Linq;

namespace UserApp.Api
{
    public static class UserApi
    {
        [FunctionName("ValidateUserForm")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "validate/userform")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var validator = new UserFormValidator();
                var json = await req.ReadAsStringAsync();
                var form = JsonConvert.DeserializeObject<UserForm>(json);
                if (form == null)
                    return new BadRequestObjectResult(new { message = "No data is submitted!" });

                var validationResult = validator.Validate(form);
                
                if (!validationResult.IsValid)
                {
                    var messages = validationResult.Errors.Select(e => new ApiValidationResultDetail()
                    {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage,
                    }).ToList();
                    return new BadRequestObjectResult(new ApiValidationResult { Type = "Error", Messages = messages });
                }

                return new OkObjectResult(new ApiValidationResult { Type = "Success", Messages = Enumerable.Empty<ApiValidationResultDetail>().ToList() });
            }
            catch (Exception ex)
            {
                log.LogError(ex,"Validate user form data");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
    }
}
