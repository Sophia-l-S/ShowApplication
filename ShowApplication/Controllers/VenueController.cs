using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using VenueApplication.Models;
using System.Web.Script.Serialization;

namespace VenueApplication.Controllers
{
    public class VenueController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
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

            string url = "listvenue";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<VenueDto> venues = response.Content.ReadAsAsync<IEnumerable<VenueDto>>().Result;


            return View(venues);
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            //objective; communicate with our venue data api to retreve a list os venues
            //change the numbers
            //crul https://localhost:44321/api/venuedata/findvenue/{id}

            string url = "findvenue/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            VenueDto selectedvenue = response.Content.ReadAsAsync<VenueDto>().Result;

            return View();
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
        public ActionResult Create(Venue venue)
        {
            //objective: add new venue into the system using api
            //crul -H "Content-Type:applacation/json" -d @venue.json https://localhost:44321/api/venuedata/addvenue
            string url = "addvenue";

            
            string jsonpayload = jss.Serialize(venue);

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

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venue/Edit/5
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

        // GET: Venue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venue/Delete/5
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
