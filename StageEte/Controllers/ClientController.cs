using Newtonsoft.Json;
using StageEte.App_Start;
using StageEte.Models;
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
    public class ClientController : ApiController
    {
        private string IP = Auth.IP;
        private int Port = 6022;
        struct IncDelVal
        {
            public int utilisateurId;
            public int code;
        }
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

        public string Post(string mobClient)
        {
            //create a client from incoming json string
            SERVICE.Lib.Client clientJson = JsonConvert.DeserializeObject<SERVICE.Lib.Client>(mobClient);
            //create a user
            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(Auth.log, Auth.pwd);
            SERVICE.Lib.User uSER = new SERVICE.Lib.User(utilisateur.Code, utilisateur.Login, utilisateur.Password);

            //ading new client
            SERVICE.RESULT_QUERY res = iCLIENT.Save(clientJson, uSER, new SERVICE.REMISES_CLIENT());

            if (res.OK)
            {
                Debug.WriteLine("added successfully");
                Debug.WriteLine(mobClient);
            }
            else
            {
                Debug.WriteLine("Error while saving : " + res.MESSAGE);
            }
            Debug.WriteLine("Error while saving : " + res.Datas);
            return res.MESSAGE;
        }


        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public string Delete(string delJson)
        {
            /// devis relation???
            //obviously a security breach, maybe will fix when I got time 
            IncDelVal incVal = JsonConvert.DeserializeObject<IncDelVal>(delJson);

            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(incVal.utilisateurId);

            SERVICE.Lib.User user = new SERVICE.Lib.User(utilisateur.Code, utilisateur.Login, utilisateur.Password);
            SERVICE.RESULT_QUERY res = iCLIENT.Supprimer(incVal.code, user);

            if (res.OK)
            {
                Debug.WriteLine("deleted successfully");
            }
            else
            {
                Debug.WriteLine("Error while deleting : " + res.MESSAGE);
            }
            return res.MESSAGE;

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
