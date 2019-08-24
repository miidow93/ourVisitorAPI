using System;
using System.Collections.Generic;

namespace OurVisitors.Models
{
    public partial class Societe
    {
        public Societe()
        {
            Visiteur = new HashSet<Visiteur>();
        }

        public int Id { get; set; }
        public string NomSociete { get; set; }
        public string Status { get; set; }
        public string Telephone { get; set; }

        public ICollection<Visiteur> Visiteur { get; set; }
    }
}
