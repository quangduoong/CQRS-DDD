﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace eShop.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(exception: e, message: e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Something went wrong.",
                Detail = "An internal server error occurred."
            };

            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(response);
        }
    }
}
