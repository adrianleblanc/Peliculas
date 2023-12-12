namespace PeliculasAPI.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivo(byte[] contenido, string extencion, string contenedor, string contentType);
        Task<string> EditarArchivo(byte[] contenido, string extencion, string contenedor, string ruta, string contentType);

        Task BorrarArchivo(string ruta, string contenedor);
    }
}
