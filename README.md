[![](https://img.shields.io/nuget/v/soenneker.zendesk.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.zendesk.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.zendesk.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.zendesk.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.zendesk.httpclients/)

# Soenneker.Zendesk.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Zendesk.HttpClients
```

## Quick start

```csharp
using Soenneker.Zendesk.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddZendeskOpenApiHttpClientAsSingleton();
```

Adds `ZendeskOpenApiHttpClient` as a singleton service.

## What you get

- `IZendeskOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `ZendeskOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ZendeskOpenApiHttpClientRegistrar.AddZendeskOpenApiHttpClientAsSingleton(services)` | Adds `ZendeskOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `ZendeskOpenApiHttpClientRegistrar.AddZendeskOpenApiHttpClientAsScoped(services)` | Adds `ZendeskOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
