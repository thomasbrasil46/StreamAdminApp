namespace StreamAdmin.Subscription.Services;

public enum PlatformCatalogValidationResult
{
    Valid,
    PlatformNotFound,
    PlanNotFound,
    PlanDoesNotBelongToPlatform,
    CatalogUnavailable
}
