namespace PeliculasAPI.Entidades
{
    public class PeliculasActores
    {
        public int PeliculaId { get; set; }
        public int ActorID { get; set; }
        public string Personaje { get; set; }
        public int Orden { get; set; }
        public Pelicula Pelicula { get; set; }
        public Actor Actor { get; set; }
    }
}
