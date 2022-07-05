using Newtonsoft.Json;
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
    public class ValuesController : ApiController
    {
        private string IP = Auth.IP;
        private int Port = 6022;
        // GET api/values
        public string Get()
        //public IEnumerable<string> Get()
        {
            return emptyJsonModel();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        public void Post(string fromMob)
        {
            //create a client from incoming json string
            SERVICE.Lib.Client clientJson = JsonConvert.DeserializeObject<SERVICE.Lib.Client>(fromMob);

            //create a user
            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(Auth.log, Auth.pwd);
            SERVICE.Lib.User uSER = new SERVICE.Lib.User(utilisateur.Code, utilisateur.Login, utilisateur.Password);

            //ading new client
            SERVICE.RESULT_QUERY res = iCLIENT.Save(clientJson, uSER, new SERVICE.REMISES_CLIENT());
            if (res.OK)
            {
                Debug.WriteLine("added successfully");
            }
            else
            {

                Debug.WriteLine("Error while saving : " + res.MESSAGE);
            }
            Debug.WriteLine(fromMob);
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
    }
}
