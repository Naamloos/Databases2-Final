using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("View")]
    public class View
    {
        [Column("Timestamp")]
        public int Timestamp { get; set; }

        [Column("ProfileId")]
        [ForeignKey("Profile")]
        [Required]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        [Column("SeriesId")]
        [ForeignKey("Series")]
        [Required]
        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}
