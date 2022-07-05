using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StageEte.Controllers
{
    public class HomeController : Controller
    {
        private string IP = "185.215.165.7";
        private int Port = 6022;
        SERVICE.ICLIENT iCLIENT
        {
            get
            {
                SERVICE.ICLIENT cLIENT = Activator.GetObject(typeof(SERVICE.ICLIENT), string.Format("TCP://{0}:{1}/{2}", IP, Port, "CLIENT")) as SERVICE.ICLIENT;
                return cLIENT;

            }
            set
            {

            }
        }
        List<SERVICE.Lib.BaseClient> Clients
        {
            get
            {

                return iCLIENT.MesClients;

            }
            set
            {

            }
        }
        public ActionResult Index()
        {

            var json = JsonConvert.SerializeObject(Clients);
            ViewBag.Title = "Home Page";
            ViewBag.Json = "number of clients" + Clients.Count + " ---- " + " " + json;
            return View();
        }
    }
}
