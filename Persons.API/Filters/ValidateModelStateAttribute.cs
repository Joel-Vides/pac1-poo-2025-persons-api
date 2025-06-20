﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Persons.API.Dtos.Common;

namespace Persons.API.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {   //Sobreescritura de metodos
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors
                            .Select(x => x.ErrorMessage)
                            .ToList()
                    );

                var responseObj = new ResponseDto<Dictionary<string, List<string>>>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "Se encontraron errores al validar la información",
                    Data = errors
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}