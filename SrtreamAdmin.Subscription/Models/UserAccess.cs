using StreamAdmin.Subscription.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Subscription.Models
{
    [Table("stream_user_access")]
    public class UserAccess : BaseEntity
    {
        [Column("usa_full_name")]
        public string UserFullName { get; set; }
        [Column("usa_email")]
        public string UserEmail { get; set; }
        [Column("usa_password")]
        public string UserPassword { get; set; }
    }
}
