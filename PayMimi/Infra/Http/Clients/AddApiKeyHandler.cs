namespace PayMimi.Infra.Http.Clients;

public class AddApiKeyHandler : DelegatingHandler
{
    private readonly ILogger<AddApiKeyHandler> _logger;

    public AddApiKeyHandler(ILogger<AddApiKeyHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
       request.Headers.Add("ApiKey", Guid.NewGuid().ToString());
        
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        
        _logger.LogInformation("Response: {Response}", response);
        
        return response;
    }
}