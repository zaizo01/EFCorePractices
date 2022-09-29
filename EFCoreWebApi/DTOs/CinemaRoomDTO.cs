using EFCoreWebApi.Entities;

namespace EFCoreWebApi.DTOs
{
    public class CinemaRoomDTO
    {
        public TypeOfCinemaRoom TypeOfCinemaRoom { get; set; }
        public decimal Price { get; set; }
    }
}
