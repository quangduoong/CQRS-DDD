﻿using eShop.Application.Behaviors;
using eShop.Application.Products.Commands;
using eShop.Application.Products.Queries;
using eShop.Application.Products.Validators;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace eShop.Api.Configurations;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, string envName)
    {
        Assembly[] actionAssemblies = {
            typeof(CreateProductCommand).GetTypeInfo().Assembly,
            typeof(GetProductByIdQuery).GetTypeInfo().Assembly
        };

        Assembly[] validationAssemblies = {
            typeof(CreateProductCommandValidator).GetTypeInfo().Assembly
        };
        services.AddValidatorsFromAssemblies(validationAssemblies);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(actionAssemblies))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(DbTransactionBehavior<,>));
    }
}

