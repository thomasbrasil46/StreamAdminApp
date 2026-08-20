using StreamAdmin.Catalog.Data.ValueObjects;

namespace StreamAdminFront.Models
{
    public class PlatformModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WebSiteUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<PlanVO> Plans { get; set; } = new List<PlanVO>();
    }
}
