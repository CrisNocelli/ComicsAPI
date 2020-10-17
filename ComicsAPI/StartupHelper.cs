using Comics.Data.Queries;
using Comics.Data.Repository;
using ComicsAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;

namespace ComicsAPI
{
    internal static class StartupHelper
    {
        private const string VersionFormat = "'v'VVV";

        internal static void SetControllerOptions(MvcOptions options)
        {
        }

        internal static void SetApiExplorerOptions(ApiExplorerOptions options)
        {
            options.GroupNameFormat = VersionFormat;
            options.SubstituteApiVersionInUrl = true;
        }

        internal static void SetApiVersionOptions(ApiVersioningOptions options)
        {
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        }

        internal static void SetDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<ICommandText, CommandText>();
            services.AddTransient<IComicCharacterRepository, ComicCharacterRepository>();
            services.AddScoped<ICharactersService, CharactersService>();
        }

        internal static IConfigureOptions<SwaggerGenOptions> SetSwaggerGenOptions(IServiceProvider serviceProvider)
        {
            return new ConfigureOptions<SwaggerGenOptions>(options =>
            {
                var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions.Where(x => !x.IsDeprecated))
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            });
        }

        internal static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"Comics Api v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
            };

            return info;
        }

        internal static void SetSwaggerUIOptions(IApiVersionDescriptionProvider provider, SwaggerUIOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
                if (!description.IsDeprecated)
                    options.SwaggerEndpoint(
                        $"{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );

            options.DocExpansion(DocExpansion.List);
        }
    }
}
