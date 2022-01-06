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
    public class OficiosController : ControllerBase
    {
        private readonly oficiosContext _context;

        public OficiosController(oficiosContext context)
        {
            _context = context;
        }

        // GET: api/Oficios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oficio>>> GetOficiosAsync()
        {
            var lst = (from o in _context.Oficios
                       select new
                       {
                           noficio = o.NOficio,
                           fecha = o.Fecha,
                           asunto=o.Asunto,
                           folio=o.FolioSolicitud,
                           capturo=o.Capturo
                       }).ToListAsync();

            return Ok(await lst);
            //return await _context.Oficios.ToListAsync();
        }

        // GET: api/Oficios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oficio>> GetOficio(long id)
        {
            var oficio = await _context.Oficios.FindAsync(id);

            if (oficio == null)
            {
                return NotFound();
            }

            return oficio;
        }

        // PUT: api/Oficios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOficio(long id, Oficio oficio)
        {
            if (id != oficio.IdOficio)
            {
                return BadRequest();
            }

            _context.Entry(oficio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OficioExists(id))
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

        // POST: api/Oficios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Oficio>> PostOficio(Oficio oficio)
        {
            _context.Oficios.Add(oficio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOficio", new { id = oficio.IdOficio }, oficio);
        }

        // DELETE: api/Oficios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOficio(long id)
        {
            var oficio = await _context.Oficios.FindAsync(id);
            if (oficio == null)
            {
                return NotFound();
            }

            _context.Oficios.Remove(oficio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OficioExists(long id)
        {
            return _context.Oficios.Any(e => e.IdOficio == id);
        }
    }
}
