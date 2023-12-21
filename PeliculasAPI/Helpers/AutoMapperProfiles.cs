using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Actor,ActoresDTO>().ReverseMap();
            CreateMap<ActoresCreacionDTO, Actor>().ForMember(x => x.Foto, op => op.Ignore());
            CreateMap<ActorDTOPatch, Actor>().ReverseMap();
        }
    }
}
