#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOficios.Data;
using WebOficios.Models;

namespace WebOficios.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoOficiosController : ControllerBase
    {
        private readonly oficiosContext _context;

        public TipoOficiosController(oficiosContext context)
        {
            _context = context;
        }

        // GET: api/TipoOficios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoOficio>>> GetTipoOficios()
        {
            var lst = (from t in _context.TipoOficios
                       select new
                       {
                           idTipo = t.IdTipo,
                           name = t.Nombre
                           
                       }).ToListAsync();

            return Ok(await lst);

            //return await _context.TipoOficios.ToListAsync();
        }

        // GET: api/TipoOficios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoOficio>> GetTipoOficio(int id)
        {
            var tipoOficio = await _context.TipoOficios.FindAsync(id);

            if (tipoOficio == null)
            {
                return NotFound();
            }

            return tipoOficio;
        }

        // PUT: api/TipoOficios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoOficio(int id, TipoOficio tipoOficio)
        {
            if (id != tipoOficio.IdTipo)
            {
                return BadRequest();
            }

            _context.Entry(tipoOficio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoOficioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoOficios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoOficio>> PostTipoOficio(TipoOficio tipoOficio)
        {
            _context.TipoOficios.Add(tipoOficio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoOficio", new { id = tipoOficio.IdTipo }, tipoOficio);
        }

        // DELETE: api/TipoOficios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoOficio(int id)
        {
            var tipoOficio = await _context.TipoOficios.FindAsync(id);
            if (tipoOficio == null)
            {
                return NotFound();
            }

            _context.TipoOficios.Remove(tipoOficio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoOficioExists(int id)
        {
            return _context.TipoOficios.Any(e => e.IdTipo == id);
        }
    }
}
