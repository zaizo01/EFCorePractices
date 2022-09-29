using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
        public CinemaOffer CinemaOffer { get; set; }
        public HashSet<CinemaRoom> CinemaRooms { get; set; }
    }
}
