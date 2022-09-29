using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Biography { get; set; }
        //[Column(TypeName = "Date")]
        public DateTime? DateOfBirth { get; set; }
        public HashSet<MovieActor> MovieActors { get; set; }
    }
}
