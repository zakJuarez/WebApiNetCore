using backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [HttpGet("listado")]
        public async Task<ActionResult<List<Autor>>> get()
        {
            return await context.Autores.Include(l => l.Libros).ToListAsync();
            //return new List<Autor>() {
            //    new Autor(){ Id = 1,Nombre = "Isaac Juarez" },
            //    new Autor(){ Id = 2, Nombre = "Juarez" }
            //};
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Autor>> PrimerAutor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(c => c.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        //[HttpGet("{id:int}/{param2}")] //=> api/autores/1/otroparametro
        //[HttpGet("{id:int}/{param2?}")] //=> api/autores/1/    ---> otroparametro puede ser opcional
        //[HttpGet("{id:int}/{param2=persona}")] //=> api/autores/1/    ---> otroparametro toma el valor de 'persona' en caso de ser nulo, o si lleva el otro parametro toma ese valor
        //public async Task<ActionResult<Autor>> Get(int id)
        //{
        //    var autor = await context.Autores.FirstOrDefaultAsync(c => c.Id == id);
        //    if(autor == null)
        //    {
        //        return NotFound(); 
        //    }
        //    return autor;
        //}

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(c => c.Nombre.ToLower().Contains(nombre.ToLower()));
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound();
            context.Remove(new Autor() { Id= id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}