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
    public class ArtistController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ArtistController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/artistdata/");
        }


        // GET: Artist/List
        public ActionResult List()
        {
            //objective; communicate with our artist data api to retreve a list os artists
            //change the numbers
            //crul https://localhost:44321/api/artistdata/listartist

            string url = "artistdata/listartist";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<ArtistDto> Artists = response.Content.ReadAsAsync<IEnumerable<ArtistDto>>().Result;


            return View(Artists);
        }

        // GET: Artist/Details/5
        public ActionResult Details(int id)
        {
            //objective; communicate with our artist data api to retreve a list os artists
            //change the numbers
            //crul https://localhost:44321/api/artistdata/findartist/{id}

            DetailsArtists ViewModel = new DetailsArtists();

            string url = "artistdata/findartist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtistDto SelectedArtist = response.Content.ReadAsAsync<ArtistDto>().Result;

            ViewModel.SelectedArtist = SelectedArtist;


            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Artist/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(Artist Artist)
        {
            //objective: add new artist into the system using api
            //crul -H "Content-Type:applacation/json" -d @artist.json https://localhost:44321/api/artistdata/addartist
            string url = "artistdata/addartist";

            
            string jsonpayload = jss.Serialize(Artist);

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

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "artistdata/findartist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtistDto selectedArtist = response.Content.ReadAsAsync<ArtistDto>().Result;


            return View(selectedArtist);
        }

        // POST: Artist/Update/5
        [HttpPost]
        public ActionResult Update(int id, Artist Artist)
        {
            string url = "artistdata/updateartist/" + id;
            string jsonpayload = jss.Serialize(Artist);
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

        // GET: Artist/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "artistdata/findartist/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ArtistDto selectedArtists = response.Content.ReadAsAsync<ArtistDto>().Result;
            return View(selectedArtists);
        }


        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "speciesdata/deletespecies/" + id;
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
