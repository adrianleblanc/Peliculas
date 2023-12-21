using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Helpers;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresControllers : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly AlmacenadorArchivosLocal almacenadorArchivosLocal;
        private readonly string contenedor = "actores";

        public ActoresControllers(ApplicationDbContext context,IMapper mapper,AlmacenadorArchivosLocal almacenadorArchivosLocal)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivosLocal = almacenadorArchivosLocal;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActoresDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacion(queryable, paginacionDTO.cantidadRegistrosPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<ActoresDTO>>(entidades);
        }

        [HttpGet("{id}", Name ="obtenerActor")]
        public async Task<ActionResult<ActoresDTO>>Get(int id, [FromQuery] PaginacionDTO paginacionDTO)
        {
            var entidad = await context.Actores.FirstOrDefaultAsync(context => context.Id == id);
            if (entidad == null) return NotFound();

            return mapper.Map<ActoresDTO>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActoresCreacionDTO actoresCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actoresCreacionDTO);

            if (actoresCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actoresCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(actoresCreacionDTO.Foto.FileName);
                    entidad.Foto = await almacenadorArchivosLocal.GuardarArchivo(contenido, extencion, contenedor, actoresCreacionDTO.Foto.ContentType);

                }
            }

            context.Add(entidad);
            await context.SaveChangesAsync();
            var dto = mapper.Map<ActoresDTO>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActoresCreacionDTO actoresCreacionDTO)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDB == null) return NotFound();
            actorDB = mapper.Map(actoresCreacionDTO, actorDB);

            if(actoresCreacionDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actoresCreacionDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extencion = Path.GetExtension(actoresCreacionDTO.Foto.FileName);
                    actorDB.Foto = await almacenadorArchivosLocal.EditarArchivo(contenido
                        , extencion, contenedor, actorDB.Foto,actoresCreacionDTO.Foto.ContentType);
                }
            }

            var entidad = mapper.Map<Actor>(actoresCreacionDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        private async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorDTOPatch> patchDocument)
        {
            if (patchDocument == null) return BadRequest();
            
            var entidadDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (entidadDB == null) return NotFound();

            var entidadDTO = mapper.Map<ActorDTOPatch>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if(!esValido) return BadRequest(ModelState);

            mapper.Map(entidadDTO, entidadDB);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Actores.AnyAsync(x => x.Id == id);
            if (!exist) return NotFound();

            context.Remove(new Actor() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
