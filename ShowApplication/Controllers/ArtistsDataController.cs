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
    public class ArtistsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArtistsData/ListArtists
        [HttpGet]
            public IEnumerable<ArtistDto> ListArtists()
        {
            List<Artist> Artists = db.Artists.ToList();
            List<ArtistDto> AnimalDtos = new List<ArtistDto>();

            Artists.ForEach(a => ArtistDtos.Add(new ArtistDto()
            {
                ArtistlID = a.ArtistlID,
                Fname = a.Fname,
                Lname = a.Lname
            }));
        }

        // GET: api/ArtistsData/FindArtist/5
        [ResponseType(typeof(Artist))]
        [HttpGet]
        public IHttpActionResult FindArtist(int id)
        {
            Artist artist = db.Artists.Find(id);
            ArtistDto AnimalDto = new ArtistDto()
            {
                ArtistlID = Artist.ArtistlID,
                Fname = Artist.Fname,
                Lname = Artist.Lname
            };

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(ArtistDto);
        }

        // POST: api/ArtistsData/updateArtist/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateArtist(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistlID)
            {
                return BadRequest();
            }

            db.Entry(artist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/ArtistsData/AddArtist
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult AddArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artists.Add(artist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artist.ArtistlID }, artist);
        }

        // DELETE: api/ArtistsData/DeleteArtist/5
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult DeleteArtist(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return NotFound();
            }

            db.Artists.Remove(artist);
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

        private bool ArtistExists(int id)
        {
            return db.Artists.Count(e => e.ArtistlID == id) > 0;
        }
    }
}