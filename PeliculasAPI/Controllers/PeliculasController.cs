using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Migrations;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/Peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IMapper mapper,IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            var peliculas = await context.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }

        [HttpGet("{id}",Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (pelicula == null) return NotFound();

            return mapper.Map<PeliculaDTO>(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            if (peliculaCreacionDTO.Poster != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extencion,contenedor,peliculaCreacionDTO.Poster.ContentType);
                }
            }
            context.Add(pelicula);
            await context.SaveChangesAsync();
            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.Id }, peliculaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculaDB = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculaDB == null) return NotFound();
            peliculaDB = mapper.Map(peliculaCreacionDTO, peliculaDB);

            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    peliculaDB.Poster = await almacenadorArchivos.EditarArchivo(contenido, extencion
                        , contenedor, peliculaDB.Poster, peliculaCreacionDTO.Poster.ContentType);
                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        private async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaDTOPatch> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var entidadDB = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (entidadDB == null) return NotFound();

            var entidadDTO = mapper.Map<PeliculaDTOPatch>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido) return BadRequest(ModelState);

            mapper.Map(entidadDTO, entidadDB);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Peliculas.AnyAsync(x => x.Id == id);
            if (!exist) return NotFound();

            context.Remove(new Pelicula() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
