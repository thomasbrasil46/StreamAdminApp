namespace StreamAdmin.Subscription.Services;

public interface IPlatformCatalogClient
{
    Task<PlatformCatalogValidationResult> ValidateAsync(
        long platformId,
        long? planId,
        CancellationToken cancellationToken = default);
}
