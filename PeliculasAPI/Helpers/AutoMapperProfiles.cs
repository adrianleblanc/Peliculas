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

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, op => op.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom());

            CreateMap<PeliculaDTOPatch, Pelicula>().ReverseMap();
        }

        private List<PeliculasGeneros> MapearPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCreacionDTO == null) return resultado;
            foreach (var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GenerosId = id });
            }
            return resultado;
        }
    }
}
