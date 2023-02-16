using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShowApplication.Models;
using System.Diagnostics;

namespace ShowApplication.Controllers
{
    public class VenueDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VenuesData/ListVenues
        [HttpGet]
        [ResponseType(typeof(VenueDto))]
        public IHttpActionResult ListVenues()
        {
            List<Venue> Venues = db.Venues.ToList();
            List<VenueDto> VenueDtos = new List<VenueDto>();

            Venues.ForEach(a => VenueDtos.Add(new VenueDto()
            {
                venueID = a.venueID,
                venueName = a.venueName,
                City = a.City,
                ProvenceState = a.ProvenceState
            }));

            return Ok(VenueDtos);
        }

        // GET: api/VenuesData/FindVenue/5
        [ResponseType(typeof(VenueDto))]
        [HttpGet]
        public IHttpActionResult FindVenue(int id)
        {
            Venue Venue = db.Venues.Find(id);
            VenueDto VenueDto = new VenueDto()
            {
                venueID = Venue.venueID,
                venueName = Venue.venueName,
                City = Venue.City,
                ProvenceState = Venue.ProvenceState
            };

            if (Venue == null)
            {
                return NotFound();
            }

            return Ok(VenueDto);
        }

        // POST: api/VenuesData/updateVenue/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVenue(int id, Venue Venue)
        {
            Debug.WriteLine("ha have reached the update venue method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != Venue.venueID)
            {
                Debug.WriteLine("id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Venue.venueID);
                return BadRequest();
            }

            db.Entry(Venue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
                {
                    Debug.WriteLine("Venue not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("none of the conditions triggerd");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VenuesData/AddVenue
        [ResponseType(typeof(Venue))]
        [HttpPost]
        public IHttpActionResult AddVenue(Venue Venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Venues.Add(Venue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Venue.venueID }, Venue);
        }

        // DELETE: api/VenuesData/DeleteVenue/5
        [ResponseType(typeof(Venue))]
        [HttpPost]
        public IHttpActionResult DeleteVenue(int id)
        {
            Venue Venue = db.Venues.Find(id);
            if (Venue == null)
            {
                return NotFound();
            }

            db.Venues.Remove(Venue);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VenueExists(int id)
        {
            return db.Venues.Count(e => e.venueID == id) > 0;
        }
    }
}