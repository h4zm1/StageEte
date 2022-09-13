using Newtonsoft.Json;
using SERVICE.Lib;
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
    public class UserController : ApiController
    {
        private string IP = Auth.IP;
        private int Port = 6022;
        // GET api/values
        public string Get()
        {
            //var json = JsonConvert.SerializeObject(Clients);
            //Debug.WriteLine("length of json string: " + json.Length);
            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(4);
            var json = JsonConvert.SerializeObject(utilisateur);
            return json;
            //return emptyJsonModel();
        }

        // GET api/values/5
        public string Get(int id)
        {

            return "value";
        }

        public string Post(string mobSign)
        {
            Debug.WriteLine("inc string" + mobSign);
            //create a user from incoming json string (can use a struct but wtv)
            SERVICE.Lib.User _user = JsonConvert.DeserializeObject<SERVICE.Lib.User>(mobSign);

            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(_user.Login, _user.Password);

            if (utilisateur == null)
            {
                Debug.WriteLine("none");
                return "none";
            }
            else
            {
                Debug.WriteLine("found  " + JsonConvert.SerializeObject(_user));
                _User user = new _User();
                user.password=_user.Password;
                user.login = _user.Login;
                user.id = _user.Id;
                user.IdUtilisateur = utilisateur.Code;//wouldn't this eventually fail? who knows!!
                var json = JsonConvert.SerializeObject(user);
                return json;
            }
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
