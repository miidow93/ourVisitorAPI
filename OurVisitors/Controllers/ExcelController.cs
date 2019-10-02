using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using FastMember;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurVisitors.Models;

namespace OurVisitors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly OurVisitorsContext _context;
        public static IHostingEnvironment _environment;

        public ExcelController(OurVisitorsContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        /*[HttpPost]
        public async Task<IEnumerable<Visiteur>> GetExcelAsync([FromBody] VmFilterDate vmFilterDate)
        {
            System.Diagnostics.Debug.WriteLine(vmFilterDate.DateEntree + " " + vmFilterDate.DateSortie);
            var visiteurs = await _context.Visiteur.Where(x => x.DateVisite >= vmFilterDate.DateEntree && x.DateVisite <= vmFilterDate.DateSortie).ToListAsync();
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(visiteurs, "Id", "nomComplet", "cinCnss", "dateVisite", "heureEntree", "heureSortie", "personneService", "idSociete", "telephone", "numBadge"))
            {
                table.Load(reader);
                System.Diagnostics.Debug.WriteLine(reader);
            }
            System.Diagnostics.Debug.WriteLine(table.Rows.Count);
            XLWorkbook wb = new XLWorkbook();
            string workSheetFormat = $"our_visitor_{DateTime.Now: yyyy-MM-dd_hh-mm-ss-fff}";
            // DateTime.Now.ToString("yyyyMMddHHmmssfff") Guid.NewGuid().ToString();
            wb.Worksheets.Add(table, workSheetFormat);
            string folderName = "Excel";
            string webRootPath = _environment.WebRootPath;
            string pathToSave = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fileName = workSheetFormat + ".xlsx";
            var fullPath = Path.Combine(pathToSave, fileName);
            wb.SaveAs(fullPath);
            return visiteurs;

        }*/

        [HttpPost]
        public async Task<IEnumerable<Visiteur>> GetExcelAsync([FromBody] VmFilterDate vmFilterDate)
        {
            System.Diagnostics.Debug.WriteLine(vmFilterDate.DateEntree + " " + vmFilterDate.DateSortie);
            var visiteurs = await _context.Visiteur.Where(x => x.DateVisite >= vmFilterDate.DateEntree && x.DateVisite <= vmFilterDate.DateSortie).ToListAsync();
            if(visiteurs.Count > 0)
            {
                DataTable table = new DataTable();
                string[] columns = { "Id", "NomComplet", "CinCnss", "DateVisite", "HeureEntree", "HeureSortie", "PersonneService", "IdSociete", "Telephone", "NumBadge" };
                using (var reader = ObjectReader.Create(visiteurs, columns))
                {
                    table.Load(reader);
                    System.Diagnostics.Debug.WriteLine(reader);
                }
                System.Diagnostics.Debug.WriteLine(table.Rows.Count);
                XLWorkbook wb = new XLWorkbook();
                string workSheetFormat = $"ourvisitor_{DateTime.Now: ddMMyyyy_hhmmssfff}";
                // DateTime.Now.ToString("yyyyMMddHHmmssfff") Guid.NewGuid().ToString();
                wb.Worksheets.Add(table, workSheetFormat);
                string folderName = "Excel";
                string webRootPath = _environment.WebRootPath;
                string pathToSave = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var fileName = workSheetFormat + ".xlsx";
                var fullPath = Path.Combine(pathToSave, fileName);
                wb.SaveAs(fullPath);
            }
            
            return visiteurs;
            // return File(fullPath, "application/octet-stream");
            // return File(fullPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

        [HttpPost("soustraitant")]
        public async Task<IEnumerable<SousTraitant>> GetExcelAsyncSousTraitant([FromBody] VmFilterDate vmFilterDate)
        {
            System.Diagnostics.Debug.WriteLine(vmFilterDate.DateEntree + " " + vmFilterDate.DateSortie);
            var sousTraitant = await _context.SousTraitant.Where(x => x.DateVisite >= vmFilterDate.DateEntree && x.DateVisite <= vmFilterDate.DateSortie).ToListAsync();
            if (sousTraitant.Count > 0)
            {
                DataTable table = new DataTable();
                string[] columns = { "Id", "NomComplet", "CinCnss", "DateVisite", "HeureEntree", "HeureSortie", "Superviseur", "Prestation", "IdSociete", "Telephone", "NumBadge" };
                using (var reader = ObjectReader.Create(sousTraitant, columns))
                {
                    table.Load(reader);
                    System.Diagnostics.Debug.WriteLine(reader);
                }
                System.Diagnostics.Debug.WriteLine(table.Rows.Count);
                XLWorkbook wb = new XLWorkbook();
                string workSheetFormat = $"soustraitant_{DateTime.Now: ddMMyyyy_hhmmssfff}";
                // DateTime.Now.ToString("yyyyMMddHHmmssfff") Guid.NewGuid().ToString();
                wb.Worksheets.Add(table, workSheetFormat);
                string folderName = "Excel";
                string webRootPath = _environment.WebRootPath;
                string pathToSave = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var fileName = workSheetFormat + ".xlsx";
                var fullPath = Path.Combine(pathToSave, fileName);
                wb.SaveAs(fullPath);
            }

            return sousTraitant;
            // return File(fullPath, "application/octet-stream");
            // return File(fullPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }

    }
}