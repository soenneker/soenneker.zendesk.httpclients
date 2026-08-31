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
    /// Adds <see cref="IZendeskOpenApiHttpClient"/> as a singleton service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddZendeskOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IZendeskOpenApiHttpClient, ZendeskOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IZendeskOpenApiHttpClient"/> as a scoped service while retaining the singleton HTTP client cache.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddZendeskOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IZendeskOpenApiHttpClient, ZendeskOpenApiHttpClient>();

        return services;
    }
}
