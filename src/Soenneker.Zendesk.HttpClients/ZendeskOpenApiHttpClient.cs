using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Zendesk.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Zendesk.HttpClients;

public sealed class ZendeskOpenApiHttpClient : IZendeskOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _clientId = $"{nameof(ZendeskOpenApiHttpClient)}:{Guid.NewGuid():N}";

    public ZendeskOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, (config: _config, baseUrl: _config.GetValueStrict<string>("Zendesk:ClientBaseUrl")), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Zendesk:Credentials");
            string authHeaderName = state.config["Zendesk:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Zendesk:AuthHeaderValueTemplate"] ?? "Basic {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl, UriKind.Absolute),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}
