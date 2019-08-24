using System;
using System.Collections.Generic;

namespace OurVisitors.Models
{
    public partial class Visiteur
    {
        public int Id { get; set; }
        public string NomComplet { get; set; }
        public string CinCnss { get; set; }
        public string PersonneService { get; set; }
        public TimeSpan? HeureEntree { get; set; }
        public TimeSpan? HeureSortie { get; set; }
        public DateTime? DateVisite { get; set; }
        public string Telephone { get; set; }
        public int? NumBadge { get; set; }
        public int? IdSociete { get; set; }

        public Societe IdSocieteNavigation { get; set; }
    }
}
