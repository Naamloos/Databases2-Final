using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Subtitle")]
    public class Subtitle
    {
        [Key]
        [Column("Id")]
        [Required]
        public int Id { get; set; }

        [Column("Language")]
        [Required]
        public string Language { get; set; }

        [Column("SeriesId")]
        [ForeignKey("Series")]
        [Required]
        public int SeriesId { get; set; }
        public Series Series { get; set; }
    }
}
