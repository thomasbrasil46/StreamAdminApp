namespace StreamAdmin.Catalog.Data.ValueObjects
{
    public class PlanVO
    {
        public long Id { get; set; }
        public long StreamingPlatformId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal ReferencePrice { get; set; }
        public string Currency { get; set; } = "BRL";
        public int? MaximumScreens { get; set; }
        public string MaximumResolution { get; set; } = null!;
        public bool HasAds { get; set; }
        public bool AllowsDownloads { get; set; }
        public bool IsActive { get; set; }
    }
}
