using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("User")]
    public class User
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Column("Email")]
        [Required]
        public string Email { get; set; }

        [Column("PasswordHash")]
        [Required]
        public string PasswordHash { get; set; }

        [Column("Activated")]
        [Required]
        public bool Activated { get; set; }

        [Column("LoginAttempts")]
        public int LoginAttempts { get; set; }

        [Column("ActivationKey")]
        [Required]
        public string ActivationKey { get; set; }

        [Column("BlockedTimestamp")]
        public long BlockedTimestamp { get; set; }

        [Column("Language")]
        [Required]
        public string Language { get; set; }

        [Column("UserInvitedById")]
        [Required]
        public int UserInvitedBytId { get; set; }
        public User InvitedBy { get; set; }
    }
}
