using StreamAdmin.Catalog.Models.Base;
using System.Net;

namespace StreamAdmin.Subscription.Services;

public class PlatformCatalogClient : IPlatformCatalogClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PlatformCatalogClient> _logger;

    public PlatformCatalogClient(HttpClient httpClient, ILogger<PlatformCatalogClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PlatformCatalogValidationResult> ValidateAsync(
        long platformId,
        long? planId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (planId.HasValue)
            {
                using HttpResponseMessage response = await _httpClient.GetAsync(
                    $"api/v1/Plan/{planId.Value}",
                    cancellationToken);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return PlatformCatalogValidationResult.PlanNotFound;

                if (!response.IsSuccessStatusCode)
                    return PlatformCatalogValidationResult.CatalogUnavailable;

                StreamingPlatform? plan = await response.Content.ReadFromJsonAsync<StreamingPlatform>(cancellationToken);
                if (plan is null)
                    return PlatformCatalogValidationResult.CatalogUnavailable;

                return plan.Id == platformId
                    ? PlatformCatalogValidationResult.Valid
                    : PlatformCatalogValidationResult.PlanDoesNotBelongToPlatform;
            }

            using HttpResponseMessage platformResponse = await _httpClient.GetAsync(
                $"api/v1/Platform/{platformId}",
                cancellationToken);

            if (platformResponse.StatusCode == HttpStatusCode.NotFound)
                return PlatformCatalogValidationResult.PlatformNotFound;

            return platformResponse.IsSuccessStatusCode
                ? PlatformCatalogValidationResult.Valid
                : PlatformCatalogValidationResult.CatalogUnavailable;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("The platform catalog request timed out.");
            return PlatformCatalogValidationResult.CatalogUnavailable;
        }
        catch (HttpRequestException exception)
        {
            _logger.LogWarning(exception, "The platform catalog could not be reached.");
            return PlatformCatalogValidationResult.CatalogUnavailable;
        }
    }

    private sealed class StreamingPlatform : BaseEntity
    {
        public long Id { get; set; }
    }
}
