namespace EFCoreWebApi.DTOs
{
    public class CinemaCreateDTO
    {
        public string Name { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public CinemaOfferDTO CinemaOffer { get; set; }
        public CinemaRoomDTO[] CinemaRooms { get; set; }
    }
}
