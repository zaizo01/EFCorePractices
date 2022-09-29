using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.DTOs
{
    public class MoviesFilterDTO
    {
        public string Title { get; set; }
        public int GenderId { get; set; }
        public bool onBillBoard { get; set; }
        public bool nextPremiere { get; set; }
    }
}
