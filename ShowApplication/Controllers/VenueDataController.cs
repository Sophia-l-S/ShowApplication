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

namespace ShowApplication.Controllers
{
    public class VenueDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VenueData
        public IQueryable<Venue> GetVenues()
        {
            return db.Venues;
        }

        // GET: api/VenueData/5
        [ResponseType(typeof(Venue))]
        public IHttpActionResult GetVenue(int id)
        {
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return NotFound();
            }

            return Ok(venue);
        }

        // PUT: api/VenueData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVenue(int id, Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venue.SpeciesID)
            {
                return BadRequest();
            }

            db.Entry(venue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VenueData
        [ResponseType(typeof(Venue))]
        public IHttpActionResult PostVenue(Venue venue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Venues.Add(venue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venue.SpeciesID }, venue);
        }

        // DELETE: api/VenueData/5
        [ResponseType(typeof(Venue))]
        public IHttpActionResult DeleteVenue(int id)
        {
            Venue venue = db.Venues.Find(id);
            if (venue == null)
            {
                return NotFound();
            }

            db.Venues.Remove(venue);
            db.SaveChanges();

            return Ok(venue);
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
            return db.Venues.Count(e => e.SpeciesID == id) > 0;
        }
    }
}