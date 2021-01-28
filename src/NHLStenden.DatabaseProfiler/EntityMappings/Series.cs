using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Series")]
    public class Series
    {
        [Key]
        [Column("Id")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Title")]
        [Required]
        public string Title { get; set; }

        [Column("Description")]
        [Required]
        public string Description { get; set; }

        [Column("AgeRestriction")]
        public int AgeRestriction { get; set; }

        [Column("IsFilm")]
        [Required]
        public bool IsFilm { get; set; }
    }
}
