﻿using Newtonsoft.Json;
using StageEte.App_Start;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace StageEte.Controllers
{
    public class DevisController : ApiController
    {
        private string IP = Auth.IP;
        private int Port = 6022;
        // GET api/values
        public string Get()
        //public IEnumerable<string> Get()
        {
            var json = JsonConvert.SerializeObject(Clients);
            Debug.WriteLine("length of json string: " + json.Length);
            return json;
            //return emptyJsonModel();
        }

        // GET api/values/5
        public string Get(int id)
        {

            return "value";
        }
       
      
        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        string emptyJsonModel()
        {
            SERVICE.Lib.Client client = new SERVICE.Lib.Client();
            var jsonFromObj = JsonConvert.SerializeObject(client);
            //Debug.WriteLine(jsonFromObj);
            return jsonFromObj;

        }


        SERVICE.IUTILISATEUR uTILISATEUR
        {
            get
            {
                SERVICE.IUTILISATEUR UTILISATEUR = Activator.GetObject(typeof(SERVICE.IUTILISATEUR), string.Format("TCP://{0}:{1}/{2}", IP, Port, "UTILISATEUR")) as SERVICE.IUTILISATEUR;
                return UTILISATEUR;
            }
            set
            {

            }
        }

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
    }
}
