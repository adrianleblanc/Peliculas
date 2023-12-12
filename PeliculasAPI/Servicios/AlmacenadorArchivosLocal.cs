
using System.Reflection.Metadata;

namespace PeliculasAPI.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath,contenedor,nombreArchivo);

                if (File.Exists(directorioArchivo)) File.Delete(directorioArchivo);
            }
        }

        public Task<string> EditarArchivo(byte[] contenido, string extencion, string contenedor, string ruta, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extencion, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extencion}";
            string folder = Path.Combine(env.WebRootPath, contenedor);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);
            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaDB = Path.Combine(urlActual,contenedor,nombreArchivo).Replace("\\","/");
            return urlParaDB;
        }
    }
}
