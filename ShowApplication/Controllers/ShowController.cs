using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using ShowApplication.Models;
//using ShowApplication.Models.ViewModels;
using System.Web.Script.Serialization;

namespace ShowApplication.Controllers
{
    public class ShowController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private object response;

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

            string url = "listshows";
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

            string url = "showdata/findshow/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            ShowDto SelectedShow = response.Content.ReadAsAsync<ShowDto>().Result;

            //ViewModel.SelectedShow = SelectedShow;

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Show/New
        public ActionResult New()
        {
            //info about all shows
            //GET api/showdata/listshows
            string url = "showsdata/listshow";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<ShowDto> ShowOptions = response.Content.ReadAsAsync<IEnumerable<ShowDto>>().Result;

            return View(ShowOptions);
        }

        // POST: Show/Create
        [HttpPost]
        public ActionResult Create(Show show)
        {
            //objective: add new show into the system using api
            //crul -H "Content-Type:applacation/json" -d @show.json https://localhost:44321/api/showdata/addshow
            string url = "showdata/addshow";
            
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
        }

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
        {
            //UpdateShow ViewModel = new UpdateShow();

            string url = "showdata/findshow" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ShowDto selectedShow = response.Content.ReadAsAsync<ShowDto>().Result;
            return View(selectedShow);
        }

        // POST: Show/Update/5
        [HttpPost]
        public ActionResult Update(int id, Show show)
        {
            string url = "showdata/updateshow/" + id;
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
        }

        // GET: Show/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "showdata/findshow/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ShowDto selectedshow = response.Content.ReadAsAsync<ShowDto>().Result;
            return View(selectedshow);

        }

        // POST: Show/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "showdata/deleteshow/" + id;
            HttpContent content = new StringContent("");
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
        }
    }
}
