using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StageEte.Models
{
    public class _Devis
    {
        public string id { get; set; }
        public int clientCode { get; set; }
        public string nom { get; set; }
        public string userCode { get; set; }
        public string Date { get; set; }
        public string code { get; set; }
        public List<_Article> listArticle { get; set; }
        public int IdUtilisateur { get; set; }
        public decimal Total { get; set; }
    }
}