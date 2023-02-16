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
    public class ShowDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ShowsData/ListShows
        [HttpGet]

        public IEnumerable<ShowDto> ListShows()
        {
            List<Show> Shows = db.Shows.ToList();
            List<ShowDto> ShowDtos = new List<ShowDto>();

            Shows.ForEach(s => ShowDtos.Add(new ShowDto()
            {
                showID = s.showID,
                artistID = s.artistID,
                venueID = s.venueID,
                DateAndTime = s.DateAndTime
            }));

            return ShowDtos;
        }

        // GET: api/ShowsData/FindShow/5
        [ResponseType(typeof(Show))]
        [HttpGet]
        public IHttpActionResult FindShow(int id)
        {
            Show Show = db.Shows.Find(id);
            ShowDto ShowDto = new ShowDto()
            {
                showID = Show.showID,
                artistID = Show.artistID,
                venueID = Show.venueID,
                DateAndTime = Show.DateAndTime
            };

            if (Show == null)
            {
                return NotFound();
            }

            return Ok(ShowDto);
        }

        // POST: api/ShowsData/updateShow/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateShow(int id, Show show)
        {
            Debug.WriteLine("ha have reached the update show method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != show.showID)
            {
                Debug.WriteLine("id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + show.showID);
                return BadRequest();
            }

            db.Entry(show).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
                {
                    Debug.WriteLine("Show not found");
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

        // POST: api/ShowsData/AddShow
        [ResponseType(typeof(Show))]
        [HttpPost]
        public IHttpActionResult AddShow(Show show)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shows.Add(show);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = show.showID }, show);
        }

        // DELETE: api/ShowsData/DeleteShow/5
        [ResponseType(typeof(Show))]
        [HttpPost]
        public IHttpActionResult DeleteShow(int id)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return NotFound();
            }

            db.Shows.Remove(show);
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

        private bool ShowExists(int id)
        {
            return db.Shows.Count(e => e.showID == id) > 0;
        }
    }
}