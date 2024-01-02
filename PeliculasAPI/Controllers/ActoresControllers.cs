using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresControllers : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivosLocal;
        private readonly string contenedor = "actores";

        public ActoresControllers(ApplicationDbContext context,IMapper mapper, IAlmacenadorArchivos almacenadorArchivosLocal):base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivosLocal = almacenadorArchivosLocal;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActoresDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await Get<Actor,ActoresDTO>(paginacionDTO);
        }

        [HttpGet("{id}", Name ="obtenerActor")]
        public async Task<ActionResult<ActoresDTO>>Get(int id)
        {
            return await Get<Actor, ActoresDTO>(id);
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
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorDTOPatch> patchDocument)
        {
            return await Patch<Actor,ActorDTOPatch>(id, patchDocument);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }
    }
}
