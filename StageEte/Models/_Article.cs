using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StageEte.Models
{
    public class _Article
    {
        public string Name { get; set; }
        public decimal Achat { get; set; }
        public string Categorie { get; set; }
        public decimal Vente { get; set; }
        public string Code { get; set; }
        public string Tva { get; set; }
        public string Designation { get; set; }
        public int Quantity { get; set; }
    }
}