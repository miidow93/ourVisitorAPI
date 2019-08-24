using System;
using System.Collections.Generic;

namespace OurVisitors.Models
{
    public partial class Regle
    {
        public int Id { get; set; }
        public int? NumOrdre { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool? Show { get; set; }
    }
}
