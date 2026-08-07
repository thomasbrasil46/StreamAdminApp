using StreamAdmin.Catalog.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Catalog.Data.ValueObjects
{
    public class PlanVO
    {
        public long StreamingPlatformId { get; set; }
        public StreamingPlatform StreamingPlatform { get; set; } = null!;
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
