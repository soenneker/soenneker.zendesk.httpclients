using Soenneker.Zendesk.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Zendesk.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ZendeskOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IZendeskOpenApiHttpClient _httpclient;

    public ZendeskOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IZendeskOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
