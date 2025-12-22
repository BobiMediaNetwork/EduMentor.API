using EduMentor.Application.Common.DTOs;
using EduMentor.Domain.Generic;
using Microsoft.AspNetCore.Http;

namespace EduMentor.Application.Common;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new ResponseType<BaseDto>
            {
                Object = null,
                Collection = null,
                Message = string.Join(" | ", ex.Errors),
                IsSuccess = false
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

