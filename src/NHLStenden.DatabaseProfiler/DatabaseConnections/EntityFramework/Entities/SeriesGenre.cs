using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Series_Genre")]
    public class SeriesGenre
    {
        [Column("SeriesId")]
        [ForeignKey("Series")]
        [Required]
        public int SeriesId { get; set; }
        public Series Series { get; set; }

        [Column("GenreId")]
        [ForeignKey("Genre")]
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
