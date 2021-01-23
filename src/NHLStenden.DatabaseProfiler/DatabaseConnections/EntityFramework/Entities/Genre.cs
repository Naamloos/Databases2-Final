using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        [Column("Id")]
        [Required]
        public int Id { get; set; }

        [Column("GenreName")]
        [Required]
        public string Name { get; set; }
    }
}
