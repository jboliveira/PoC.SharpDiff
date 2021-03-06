﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Swagger
{
    /// <summary>
    /// Define versioned Swagger document.
    /// For more info, see https://github.com/Microsoft/aspnet-api-versioning/wiki/API-Documentation
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:PoC.SharpDiff.WebAPI.Infrastructure.Swagger.ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">Provider <see cref="IApiVersionDescriptionProvider"/>.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Configure the Swagger options.
        /// </summary>
        /// <param name="options">Options <see cref="SwaggerGenOptions"/>.</param>
        public void Configure(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();

            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddFluentValidationRules();
        }

        /// <summary>
        /// Creates the info for API version.
        /// </summary>
        /// <returns>The info for API version.</returns>
        /// <param name="description">Description <see cref="ApiVersionDescription"/>.</param>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"{Program.AppName} {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "PoC SharpDiff ASP.NET Core Web API",
                Contact= new OpenApiContact
                {
                    Name = "Jader Oliveira",
                    Email = "jader.bueno@yahoo.ie"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
