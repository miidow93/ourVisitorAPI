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
    public class VisiteursController : ControllerBase
    {
        private readonly OurVisitorsContext _context;

        public VisiteursController(OurVisitorsContext context)
        {
            _context = context;
        }

        // GET: api/Visiteurs
        [HttpGet]
        public IEnumerable<Visiteur> GetVisiteur()
        {
            return _context.Visiteur;
        }

        // GET: api/Visiteurs
        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<Visiteur>>> GetVisiteurToday()
        {
            return await _context.Visiteur.Where(e => e.DateVisite == DateTime.Now.Date)
                .Include(l => l.IdSocieteNavigation)
                .ToListAsync();
        }

        // GET: api/Visiteurs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisiteur([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visiteur = await _context.Visiteur.FindAsync(id);

            if (visiteur == null)
            {
                return NotFound();
            }

            return Ok(visiteur);
        }

        // PUT: api/Visiteurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisiteur([FromRoute] int id, [FromBody] Visiteur visiteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visiteur.Id)
            {
                return BadRequest();
            }

            _context.Entry(visiteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisiteurExists(id))
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

        // POST: api/Visiteurs
        [HttpPost]
        public async Task<IActionResult> PostVisiteur([FromBody] Visiteur visiteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Visiteur.Add(visiteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisiteur", new { id = visiteur.Id }, visiteur);
        }

        [HttpPost("postev")]
        public async Task<ActionResult<Visiteur>> PostVisiteur(VmVisiteur vmvisiteur)
        {
            var idsociete = SocieteExists(vmvisiteur.Societe);
            Visiteur visiteur = new Visiteur()
            {
                DateVisite = DateTime.Now,
                HeureEntree = DateTime.Now.TimeOfDay,
                NomComplet = vmvisiteur.NomComplet,
                CinCnss = vmvisiteur.CinCnss,
                PersonneService = vmvisiteur.PersonneService,
                Telephone = vmvisiteur.Telephone,
                NumBadge = vmvisiteur.NumBadge,
                IdSociete = idsociete,
            };
            _context.Visiteur.Add(visiteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisiteur", new { id = visiteur.Id }, visiteur);

        }

        [HttpGet("sortie/{id}")]
        public async Task<ActionResult> SortieVisiteur([FromRoute] int id)
        {
            Visiteur visiteur = await _context.Visiteur.FirstOrDefaultAsync(x => x.Id == id);
            if (visiteur != null)
            {
                visiteur.HeureSortie = DateTime.Now.TimeOfDay;
                _context.SaveChanges();
            }
            return Ok(new {
                heureSorite = visiteur.HeureSortie
            });
        }

        
        // DELETE: api/Visiteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisiteur([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visiteur = await _context.Visiteur.FindAsync(id);
            if (visiteur == null)
            {
                return NotFound();
            }

            _context.Visiteur.Remove(visiteur);
            await _context.SaveChangesAsync();

            return Ok(visiteur);
        }

        private bool VisiteurExists(int id)
        {
            return _context.Visiteur.Any(e => e.Id == id);
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