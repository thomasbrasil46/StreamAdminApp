using StreamAdmin.Catalog.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Catalog.Models
{
    [Table("stream_platforms")]
    public class StreamingPlatform : BaseEntity
    {
        [Column("sp_name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = null!;

        [Column("sp_description")]
        [Required]
        [StringLength(250)]
        public string Description { get; set; } = null!;

        [Column("sp_websiteurl")]
        [Required]
        public string WebSiteUrl { get; set; } = null!;

        [Column("sp_isactive")]
        [Required]
        public bool IsActive { get; set; }

        public ICollection<StreamingPlan> Plans { get; set; } = new List<StreamingPlan>();


        //ToDo: Add Logo property to the StreamingPlatform class. Check if is possible to add a blob or byte array to the database. If not, consider storing the logo as a URL or base64 string.
        //public string Logo { get; set; }        
    }
}
