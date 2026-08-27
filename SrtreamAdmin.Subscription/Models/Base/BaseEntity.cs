using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Subscription.Models.Base;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id { get; set; }
}
