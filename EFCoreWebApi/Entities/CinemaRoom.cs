using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities
{
    public class CinemaRoom
    {
        public int Id { get; set; }
        public TypeOfCinemaRoom TypeOfCinemaRoom { get; set; }
        public decimal Price { get; set; }
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        public HashSet<Movie> Movies { get; set; }
    }
}

