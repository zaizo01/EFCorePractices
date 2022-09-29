using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime PremiereDate { get; set; }
        public string PosterURL { get; set; }
        public HashSet<Gender> Genders { get; set; }
        public HashSet<CinemaRoom> CinemaRooms { get; set; }
        public HashSet<MovieActor> MovieActors { get; set; }
    }
}
