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
    public class VenueController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        public object SelectedVenue { get; private set; }
        public object ViewModel { get; private set; }

        static VenueController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/venuedata/");
        }


        // GET: Venue/List
        public ActionResult List()
        {
            //objective; communicate with our venue data api to retreve a list os venues
            //change the numbers
            //crul https://localhost:44321/api/venuedata/listvenue

            string url = "listvenues";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<VenueDto> venues = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result;


            return View(venues);
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            //objective; communicate with our venue data api to retreve a list os venues
            //crul https://localhost:44321/api/venuedata/findvenue/{id}

            string url = "findvenue/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            VenueDto Selectedvenue = response.Content.ReadAsAsync<VenueDto>().Result;

           //ViewModel.SelectedVenue = SelectedVenue;

            url = "showdata/listvenueforshow/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<ShowDto> KeptShow = response.Content.ReadAsAsync<IEnumerable<ShowDto>>().Result;

            //ViewModel.KeptShows = KeptShow;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Venue/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Venue/Create
        [HttpPost]
        public ActionResult Create(Venue Venue)
        {
            //objective: add new venue into the system using api
           //crul -H "Content-Type:applacation/json" -d @venue.json https://localhost:44321/api/venuedata/addvenue
            string url = "addvenue";
            string jsonpayload = jss.Serialize(Venue);

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

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "findvenue/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VenueDto selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;

            return View(selectedVenue);
        }

        // POST: Venue/Update/5
        [HttpPost]
        public ActionResult Update(int id, Venue Venue)
        {
           string url = "updatevenue/" + id;
           string jsonpayload = jss.Serialize(Venue);
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

        // GET: Venue/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findvenue/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VenueDto selectedVenue = response.Content.ReadAsAsync<VenueDto>().Result;
            return View(selectedVenue);
        }

        // POST: Venue/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletevenue/" + id;
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
