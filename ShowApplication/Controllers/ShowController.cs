using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using ShowApplication.Models;
using System.Web.Script.Serialization;

namespace ShowApplication.Controllers
{
    public class ShowController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ShowController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/showdata/");
        }


        // GET: Show/List
        public ActionResult List()
        {
            //objective; communicate with our show data api to retreve a list os shows
            //change the numbers
            //crul https://localhost:44321/api/showdata/listshow

            string url = "listshow";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ShowDto> shows = response.Content.ReadAsAsync<IEnumerable<ShowDto>>().Result;


            return View(shows);
        }

        // GET: Show/Details/5
        public ActionResult Details(int id)
        {
            //objective; communicate with our show data api to retreve a list os shows
            //change the numbers
            //crul https://localhost:44321/api/showdata/findshow/{id}

            string url = "findshow/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ShowDto selectedshow = response.Content.ReadAsAsync<ShowDto>().Result;

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Show/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Show/Create
        [HttpPost]
        public ActionResult Create(Show show)
        {
            //objective: add new show into the system using api
            //crul -H "Content-Type:applacation/json" -d @show.json https://localhost:44321/api/showdata/addshow
            string url = "addshow";

            
            string jsonpayload = jss.Serialize(show);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("List");
        }

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Show/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Show/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Show/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
