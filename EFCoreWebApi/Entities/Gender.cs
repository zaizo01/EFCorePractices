using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities
{
    //[Table("TablaGeneros", Schema = "Peliculas")]
    public class Gender
    {
        //[Key]
        public int Identificador { get; set; }
        //public int GenderId { get; set; }
        //[StringLength(150)]
        //[Required]
        //[MaxLength(150)]
        //[Column("NombreGenero")]
        public string Name { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}
