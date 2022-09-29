using AutoMapper;
using EFCoreWebApi.DTOs;
using EFCoreWebApi.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Services
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();
            CreateMap<Gender, GenderDTO>();

            // without projectTo
            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.Cinemas, ent => ent.MapFrom(prop => prop.CinemaRooms.Select(s => s.Cinema)))
                .ForMember(dto => dto.Actors, ent => ent.MapFrom(prop => prop.MovieActors.Select(s => s.Actor)));

            // with ProjectTo
            //CreateMap<Movie, MovieDTO>()
            //   .ForMember(dto => dto.Genders, ent => ent.MapFrom(prop => prop.Genders.OrderByDescending(g => g.Name)))
            //   .ForMember(dto => dto.CinemaRooms, ent => ent.MapFrom(prop => prop.CinemaRooms.Select(s => s.Cinema)))
            //   .ForMember(dto => dto.MovieActors, ent => ent.MapFrom(prop => prop.MovieActors.Select(s => s.Actor)));

            CreateMap<Cinema, CinemaDTO>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Location.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Location.X));

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<CinemaCreateDTO, Cinema>()
                .ForMember(ent => ent.Location, 
                    dto => dto.MapFrom(campo =>
                        geometryFactory.CreatePoint(new Coordinate(campo.Longitud, campo.Latitud))));

            CreateMap<CinemaOfferDTO, CinemaOffer>();
            CreateMap<CinemaRoomDTO, CinemaRoomDTO>();
        }
    }
}
