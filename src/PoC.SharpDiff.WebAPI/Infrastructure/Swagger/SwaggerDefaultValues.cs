using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PoC.SharpDiff.WebAPI.Infrastructure.Swagger
{
    /// <summary>  
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.  
    /// </summary>  
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.  
    /// Once they are fixed and published, this class can be removed.</remarks>  
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary> Applies the filter to the specified operation using the given context. </summary>
        /// <param name="operation">The operation <see cref="OpenApiOperation"/> to apply the filter to.</param>
        /// <param name="context">The current operation filter context <see cref="OperationFilterContext"/>.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApplyConsumes(operation);
            ApplyParameters(operation, context);
        }

        /// <summary>
        /// Applies the consumes.
        /// </summary>
        /// <param name="operation">Operation <see cref="OpenApiOperation"/>.</param>
        private void ApplyConsumes(OpenApiOperation operation)
        {
            /*if (operation.Consumes == null)
            {
                return;
            }

            operation.Consumes.Clear();
            operation.Consumes = new List<string> { MediaTypeNames.Application.Json };*/
        }

        /// <summary>
        /// Applies the parameters.
        /// </summary>
        /// <param name="operation">Operation <see cref="OpenApiOperation"/>.</param>
        /// <param name="context">Context <see cref="OperationFilterContext"/>.</param>
        private void ApplyParameters(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }
            /*
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }*/
        }
    }
}
