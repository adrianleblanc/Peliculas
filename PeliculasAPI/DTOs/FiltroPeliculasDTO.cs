namespace PeliculasAPI.DTOs
{
    public class FiltroPeliculasDTO
    {
        public int Pagina { get; set; }
        public int cantidadRegistrosPorPagina { get; set; } = 10;
        public PaginacionDTO Paginacion 
        {
            get { return new PaginacionDTO() { Pagina = Pagina, cantidadRegistrosPorPagina = cantidadRegistrosPorPagina }; }
        }

        public string Titulo {  get; set; }
        public int GeneroId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }
        public string CampoOrdenar { get; set; }
        public bool OrdenAscendente { get; set; } = true;
    }
}
