[![](https://img.shields.io/nuget/v/soenneker.zendesk.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.zendesk.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.zendesk.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.zendesk.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.zendesk.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.zendesk.httpclients/actions/workflows/codeql.yml)

# Soenneker.Zendesk.HttpClients

Provides a cached `HttpClient` configured for one Zendesk account and authentication credential.

## Install

```shell
dotnet add package Soenneker.Zendesk.HttpClients
```

## Configuration

For API-token authentication, Base64-encode the UTF-8 value `{email_address}/token:{api_token}` and supply the result as `Credentials`:

```json
{
  "Zendesk": {
    "ClientBaseUrl": "https://acme.zendesk.com/",
    "Credentials": "base64-encoded-credentials"
  }
}
```

The default header is `Authorization: Basic {token}`. OAuth can be configured without changing code:

```json
{
  "Zendesk": {
    "ClientBaseUrl": "https://acme.zendesk.com/",
    "Credentials": "oauth-access-token",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

`AuthHeaderName` can override the header name when required. `ClientBaseUrl` should be the account origin; generated API paths already include `/api/v2`.

## Registration

```csharp
using Soenneker.Zendesk.HttpClients.Registrars;

services.AddZendeskOpenApiHttpClientAsSingleton();
```

Scoped registration is also available:

```csharp
services.AddZendeskOpenApiHttpClientAsScoped();
```

## Usage

```csharp
public sealed class ZendeskTicketReader
{
    private readonly IZendeskOpenApiHttpClient _zendesk;

    public ZendeskTicketReader(IZendeskOpenApiHttpClient zendesk)
    {
        _zendesk = zendesk;
    }

    public async Task<string> GetTickets(CancellationToken cancellationToken)
    {
        HttpClient client = await _zendesk.Get(cancellationToken);
        return await client.GetStringAsync("api/v2/tickets.json", cancellationToken);
    }
}
```

Each provider owns its cached named client. Disposing the provider removes that client from the shared cache; disposing the cache itself remains the responsibility of its DI lifetime.
