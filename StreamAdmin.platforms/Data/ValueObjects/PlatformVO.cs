using StreamAdmin.Catalog.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Catalog.Data.ValueObjects
{
    public class PlatformVO
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string WebSiteUrl { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<StreamingPlan> Plans { get; set; } = null!;
    }
}
