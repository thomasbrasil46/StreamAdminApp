using StreamAdmin.Catalog.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Catalog.Models
{
    [Table("streaming_plans")]
    public class StreamingPlan : BaseEntity
    {
        [Column("spln_streamingplatformid")]
        public long StreamingPlatformId { get; set; }
        public StreamingPlatform StreamingPlatform { get; set; } = null!;

        [Column("spln_name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [Column("spln_description")]
        [Required]
        [StringLength(250)]
        public string Description { get; set; } = null!;

        [Column("spln_referenceprice")]
        [Required]
        public decimal ReferencePrice { get; set; }

        [Column("spln_currency")]
        [StringLength(3)]
        public string Currency { get; set; } = "BRL";

        [Column("spln_maximumscreens")]
        public int? MaximumScreens { get; set; }

        [Column("spln_maximumresolution")]
        [StringLength(30)]
        public string MaximumResolution { get; set; } = null!;

        [Column("spln_hasads")]
        [Required]
        public bool HasAds { get; set; }

        [Column("spln_allowsdownloads")]
        [Required]
        public bool AllowsDownloads { get; set; }

        [Column("spln_isactive")]
        [Required]
        public bool IsActive { get; set; }
    }
}
