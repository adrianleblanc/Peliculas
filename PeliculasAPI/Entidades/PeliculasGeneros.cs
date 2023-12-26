namespace PeliculasAPI.Entidades
{
    public class PeliculasGeneros
    {
        public int PeliculasId { get; set; }
        public int GenerosId { get; set; }
        public Genero Genero { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
