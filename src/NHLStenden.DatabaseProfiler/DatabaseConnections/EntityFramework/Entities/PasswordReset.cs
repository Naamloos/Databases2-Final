using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("PasswordReset")]
    public class PasswordReset
    {
        [Column("PasswordResetKey")]
        public string PasswordResetKey { get; set; }

        [Column("ExpirationTimestamp")]
        public long ExpirationTimesstamp { get; set; }

        [Column("UserId")]
        [Key]
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
