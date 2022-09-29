using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.DTOs
{
    public class CinemaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
