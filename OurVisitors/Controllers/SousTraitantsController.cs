using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurVisitors.Models;

namespace OurVisitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SousTraitantsController : ControllerBase
    {
        private readonly OurVisitorsContext _context;

        public SousTraitantsController(OurVisitorsContext context)
        {
            _context = context;
        }

        // GET: api/SousTraitants
        [HttpGet]
        public IEnumerable<SousTraitant> GetSousTraitant()
        {
            return _context.SousTraitant;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SousTraitant>>> GetAllSousTraitant()
        {
            return await _context.SousTraitant
                .Include(l => l.IdSocieteNavigation)
                .ToListAsync();
        }

        // GET: api/Visiteurs
        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<SousTraitant>>> GetSousTraitantToday()
        {
            return await _context.SousTraitant.Where(e => e.DateVisite == DateTime.Now.Date)
                .Include(l => l.IdSocieteNavigation)
                .ToListAsync();
        }

        // GET: api/SousTraitants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSousTraitant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sousTraitant = await _context.SousTraitant.FindAsync(id);

            if (sousTraitant == null)
            {
                return NotFound();
            }

            return Ok(sousTraitant);
        }

        // PUT: api/SousTraitants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSousTraitant([FromRoute] int id, [FromBody] SousTraitant sousTraitant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sousTraitant.Id)
            {
                return BadRequest();
            }

            _context.Entry(sousTraitant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SousTraitantExists(id))
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

        // POST: api/SousTraitants
        [HttpPost]
        public async Task<IActionResult> PostSousTraitant([FromBody] SousTraitant sousTraitant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SousTraitant.Add(sousTraitant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSousTraitant", new { id = sousTraitant.Id }, sousTraitant);
        }


        [HttpPost("postev")]
        public async Task<ActionResult<SousTraitant>> PostSousTraitant(VmSousTraitant vmSousTraitant)
        {
            var idSociete = SocieteExists(vmSousTraitant.Societe);
            SousTraitant sousTraitant = new SousTraitant()
            {
                DateVisite = DateTime.Now,
                HeureEntree = DateTime.Now.TimeOfDay,
                NomComplet = vmSousTraitant.NomComplet,
                CinCnss = vmSousTraitant.CinCnss,
                Superviseur = vmSousTraitant.Superviseur,
                Prestation = vmSousTraitant.Prestation,
                Telephone = vmSousTraitant.Telephone,
                NumBadge = vmSousTraitant.NumBadge,
                IdSociete = idSociete,
            };
            System.Diagnostics.Debug.WriteLine(sousTraitant.Prestation);
            _context.SousTraitant.Add(sousTraitant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSousTraitant", new { id = sousTraitant.Id }, sousTraitant);

        }

        [HttpGet("sortie/{id}")]
        public async Task<ActionResult> SortieSousTraitant([FromRoute] int id)
        {
            SousTraitant sousTraitant = await _context.SousTraitant.FirstOrDefaultAsync(x => x.Id == id);
            if (sousTraitant != null)
            {
                sousTraitant.HeureSortie = DateTime.Now.TimeOfDay;
                _context.SaveChanges();
            }
            return Ok(new
            {
                heureSortie = sousTraitant.HeureSortie
            });
        }

        // DELETE: api/SousTraitants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSousTraitant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sousTraitant = await _context.SousTraitant.FindAsync(id);
            if (sousTraitant == null)
            {
                return NotFound();
            }

            _context.SousTraitant.Remove(sousTraitant);
            await _context.SaveChangesAsync();

            return Ok(sousTraitant);
        }

        private bool SousTraitantExists(int id)
        {
            return _context.SousTraitant.Any(e => e.Id == id);
        }

        private int SocieteExists(string namesociete)
        {
            if (!_context.Societe.Any(e => e.NomSociete.Equals(namesociete)))
            {
                Societe societe = new Societe()
                {
                    NomSociete = namesociete
                };
                _context.Societe.Add(societe);
                _context.SaveChanges();
                return societe.Id;
            }
            return _context.Societe.Where(e => e.NomSociete.Equals(namesociete)).FirstOrDefault().Id;
        }
    }
}