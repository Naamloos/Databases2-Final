using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Episode")]
    public class Episode
    {
        [Column("EpisodeOrder")]
        [Required]
        public int EpisodeOrder { get; set; }

        [ForeignKey("Series")]
        [Column("SeriesId")]
        [Required]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }
    }
}
