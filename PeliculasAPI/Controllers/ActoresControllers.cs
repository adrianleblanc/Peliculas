using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresControllers : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresControllers(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActoresDTO>>> Get()
        {
            var entidades = await context.Actores.ToListAsync();
            return mapper.Map<List<ActoresDTO>>(entidades);
        }

        [HttpGet("{id}", Name ="obtenerActor")]
        public async Task<ActionResult<ActoresDTO>>Get(int id)
        {
            var entidad = context.Actores.FirstOrDefaultAsync(context => context.Id == id);
            if (entidad == null) return NotFound();

            return mapper.Map<ActoresDTO>(entidad);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActoresCreacionDTO actoresCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actoresCreacionDTO);
            context.Add(entidad);
            //await context.SaveChangesAsync();
            var dto = mapper.Map<ActoresDTO>(entidad);
            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActoresCreacionDTO actoresCreacionDTO)
        {
            var entidad = mapper.Map<Actor>(actoresCreacionDTO);
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
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
