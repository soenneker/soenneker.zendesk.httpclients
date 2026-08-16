using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Zendesk.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Zendesk.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class ZendeskOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="ZendeskOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddZendeskOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IZendeskOpenApiHttpClient, ZendeskOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ZendeskOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddZendeskOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IZendeskOpenApiHttpClient, ZendeskOpenApiHttpClient>();

        return services;
    }
}
