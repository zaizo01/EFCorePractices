using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<GenderDTO> Genders { get; set; }
        public ICollection<CinemaDTO> Cinemas { get; set; }
        public ICollection<ActorDTO> Actors { get; set; }
    }
}
