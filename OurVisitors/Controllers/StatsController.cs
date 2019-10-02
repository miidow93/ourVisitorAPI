using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class StatsController : ControllerBase
    {
        private readonly OurVisitorsContext _context;

        public StatsController(OurVisitorsContext context)
        {
            _context = context;
        }

        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> GetStatsByMonth([FromRoute] int month, [FromRoute] int year)
        {
            var monthStats = await _context.Visiteur.Where(v => v.DateVisite.Value.Month == month && v.DateVisite.Value.Year == year)
                                                .GroupBy(g => g.DateVisite.Value.Day)
                                                .Select(s => new { label = s.Key, count = s.Count() }).ToListAsync();



            return Ok(new { stats = monthStats });
        }

        /*[HttpGet]
        public async Task<IActionResult> GetStatsByYear()
        {
            var yearStats = _context.Visiteur.Where(w => w.DateVisite != null)
                                                .GroupBy(g => g.DateVisite.Value.Year)
                                                .Select(s => new { year = s.Key, count = s.Count() });


                                                
            return Ok(new { stats = yearStats });
        }*/


        [HttpGet("{year}")]
        public async Task<IActionResult> GetStatsByYear([FromRoute] int year)
        {
            var yearStats = _context.Visiteur.Where(w => w.DateVisite.Value.Year == year)
                                                .GroupBy(g => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.DateVisite.Value.Month))
                                                .Select(s => new { label = s.Key, count = s.Count() })
                                                .OrderBy(o => DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(o.label) + 1);



            return Ok(new { stats = yearStats });
        }

        [HttpGet("sousTraitant/{month}/{year}")]
        public async Task<IActionResult> GetStatsByMonthSousTraitant([FromRoute] int month, [FromRoute] int year)
        {
            var monthStats = await _context.SousTraitant.Where(v => v.DateVisite.Value.Month == month && v.DateVisite.Value.Year == year)
                                                .GroupBy(g => g.DateVisite.Value.Day)
                                                .Select(s => new { label = s.Key, count = s.Count() }).ToListAsync();



            return Ok(new { stats = monthStats });
        }

        [HttpGet("sousTraitant/{year}")]
        public async Task<IActionResult> GetStatsByYearSousTraitant([FromRoute] int year)
        {
            var yearStats = await _context.SousTraitant.Where(w => w.DateVisite.Value.Year == year)
                                                .GroupBy(g => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.DateVisite.Value.Month))
                                                .Select(s => new { label = s.Key, count = s.Count() })
                                                .OrderBy(o => DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(o.label) + 1).ToListAsync();



            return Ok(new { stats = yearStats });
        }


    }
}