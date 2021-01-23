using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Profile")]
    public class Profile
    {
        [Key]
        [Column("Id")]
        [Required]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column("UserId")]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("ProfilePhoto")]
        public string ProfilePhoto { get; set; }

        [Column("Age")]
        [Required]
        public int Age { get; set; }
    }
}
