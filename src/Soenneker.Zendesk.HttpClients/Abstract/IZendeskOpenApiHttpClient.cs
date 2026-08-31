using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Zendesk.HttpClients.Abstract;

/// <summary>
/// Provides a cached HTTP client configured for one Zendesk account and authentication credential.
/// </summary>
public interface IZendeskOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configured Zendesk HTTP client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
