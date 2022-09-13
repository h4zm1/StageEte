using Newtonsoft.Json;
using SERVICE;
using SERVICE.Lib;
using StageEte.App_Start;
using StageEte.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        public string GetArticle()
        {

            List<_Article> listArt = new List<_Article>();
            foreach (SERVICE.Lib.Article art in IARTICLE.Articles(true))
            {
                _Article arti = new _Article();


                arti.Name = art.Name;
                arti.Achat = art.Achat;
                arti.Categorie = art.Categorie.ToString();
                arti.Vente = art.Vente.ToString();
                arti.Code = art.Code;
                arti.Tva = art.Tva.ToString();
                arti.Designation = art.Designation;
                arti.Quantity = 1;
                listArt.Add(arti);
            }


            var json = JsonConvert.SerializeObject(listArt);



            Debug.WriteLine("length of json string: " + json.Length);
            return json;
        }

        //GET api/values/5
        public string Get(int id)
        {

            //List<SERVICE.Lib.Devis> listDev = IDEVIS.Liste(DateTime.Today.AddDays(-30), DateTime.Now, 0, 0);
            //foreach (SERVICE.Lib.Devis dev in listDev)
            //{
            //    Debug.WriteLine("last 7 days devis code: " + dev.Id);

            //}
            //var json = JsonConvert.SerializeObject(listDev);
            return json;
        }

        public string Post(string days)
        {
            int idays = int.Parse(days);
            List<SERVICE.Lib.Devis> timedListDev = IDEVIS.Liste(DateTime.Today.AddDays(-idays), DateTime.Now, 0, 0);
            List<_Devis> listDevis = new List<_Devis>();

            foreach (SERVICE.Lib.Devis dev in timedListDev)
            {
                _Devis devi = new _Devis();
                devi.code = dev.Code.ToString();
                devi.id = dev.Id.ToString();
                devi.userCode = dev.Code_user.ToString();
                devi.Date = dev.Date.ToString("dd/MMMM/yyyy");
                devi.nom = dev.Nom.ToString();
                devi.clientCode = dev.Client.Code;

                listDevis.Add(devi);
            }
            var json = JsonConvert.SerializeObject(listDevis);
            return json;

        }
        // PUT api/values/5
        public string Put(string value)
        {
            //_devis contains all the (raw) data needed to register a devis
            _Devis _devis = JsonConvert.DeserializeObject<_Devis>(value);

            SERVICE.Lib.Utilisateur utilisateur = uTILISATEUR.Utilisateur(_devis.IdUtilisateur);

            SERVICE.Lib.User user = new SERVICE.Lib.User(utilisateur.Code, utilisateur.Login, utilisateur.Password);

            //the devis that will get registered
            SERVICE.Lib.Devis devis = new SERVICE.Lib.Devis();

            //so it seems that client code is used as an ID too
            SERVICE.Lib.Client client = iCLIENT.Client(_devis.clientCode, user);

            devis.Client = client;
            devis.Attes_exo = "";
            foreach (_Article _article in _devis.listArticle)
            {//article got min 1 quantity
                for (int i = 0; i < _article.Quantity; i++)
                {

                    //there's some shared propreties and it seems they're getting auto assigned correctly??
                    var json = JsonConvert.SerializeObject(_article);
                    Article art = JsonConvert.DeserializeObject<Article>(json);
                    devis.AddArticle(art);
                }

            }
            

            SERVICE.RESULT_QUERY res = IDEVIS.Save(devis, user);
            Debug.WriteLine("MESSAGE:: "+res.ToString());
            return res.MESSAGE;

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            ///SERVICE.RESULT_QUERY res = IDEVIS.Delete(159, uSER);

        }
        string emptyJsonModel()
        {
            SERVICE.Lib.Client client = new SERVICE.Lib.Client();
            var jsonFromObj = JsonConvert.SerializeObject(client);
            //Debug.WriteLine(jsonFromObj);
            return jsonFromObj;

        }


        SERVICE.IDEVIS IDEVIS
        {
            get
            {
                SERVICE.IDEVIS DEVIS = Activator.GetObject(typeof(SERVICE.IDEVIS), string.Format("TCP://{0}:{1}/{2}", IP, Port, "DEVIS")) as SERVICE.IDEVIS;
                return DEVIS;
            }
            set
            {

            }
        }
        SERVICE.IARTICLE IARTICLE
        {
            get
            {
                SERVICE.IARTICLE ARTICLE = Activator.GetObject(typeof(SERVICE.IARTICLE), string.Format("TCP://{0}:{1}/{2}", IP, Port, "ARTICLE")) as SERVICE.IARTICLE;
                return ARTICLE;
            }
            set
            {

            }
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
