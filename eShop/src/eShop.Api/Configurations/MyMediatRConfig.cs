﻿using eShop.Application.Products.Commands;
using eShop.Application.Products.Queries;
using System.Reflection;

namespace eShop.Application.Configurations;

public static class MyMediatRConfig
{
    public static void AddMyMediatRConfig(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        Assembly[] assemblies = {
            typeof(CreateProductCommand).GetTypeInfo().Assembly,
            typeof(GetProductByIdQuery).GetTypeInfo().Assembly
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
    }
}