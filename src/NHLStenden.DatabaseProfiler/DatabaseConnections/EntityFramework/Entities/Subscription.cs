using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Subscription")]
    public class Subscription
    {
        [Column("SubscriptionType")]
        public string SubscriptionType { get; set; }

        [Column("EndTimestamp")]
        public long EndTimestamp { get; set; }

        [Column("UserId")]
        [ForeignKey("User")]
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
