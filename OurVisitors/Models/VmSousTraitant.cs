using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurVisitors.Models
{
    public class VmSousTraitant
    {
        public string NomComplet { get; set; }
        public string CinCnss { get; set; }
        public string Superviseur { get; set; }
        public string Prestation { get; set; }
        public string Telephone { get; set; }
        public int? NumBadge { get; set; }
        public string Societe { get; set; }
    }
}
