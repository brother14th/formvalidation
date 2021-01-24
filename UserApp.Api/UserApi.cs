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
using System.Collections.Generic;

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
                var messages = new List<ApiValidationResultDetail> { new ApiValidationResultDetail { Field = "NA", Error = "Error validating data!" } };
                return new BadRequestObjectResult(new ApiValidationResult { Type = "Error", Messages = messages });
            }
        }
        
    }
}
