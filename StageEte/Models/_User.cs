using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StageEte.Models
{
    public class _User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int IdUtilisateur { get; set; }
    }
}