using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("WatchlistItem")]
    public class WatchlistItem
    {
        [Column("ProfileId")]
        [Required]
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        [Column("SeriesId")]
        [Required]
        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}
