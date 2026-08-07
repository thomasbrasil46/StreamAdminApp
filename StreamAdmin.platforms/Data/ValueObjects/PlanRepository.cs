using StreamAdmin.Catalog.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Catalog.Data.ValueObjects
{
    public class PlanRepository
    {
        public long StreamingPlatformId { get; set; }
        public StreamingPlatform StreamingPlatform { get; set; } = null!;
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ReferencePrice { get; set; }
        public string Currency { get; set; } = "BRL";
        public int? MaximumScreens { get; set; }
        public string MaximumResolution { get; set; }
        public bool HasAds { get; set; }
        public bool AllowsDownloads { get; set; }
        public bool IsActive { get; set; }
    }
}
