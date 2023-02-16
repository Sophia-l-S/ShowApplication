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
    public class ArtistDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArtistsData/ListArtists
        [HttpGet]
        [ResponseType(typeof(ArtistDto))]
        public IHttpActionResult ListArtists()
        {
            List<Artist> Artists = db.Artists.ToList();
            List<ArtistDto> ArtistDtos = new List<ArtistDto>();

            Artists.ForEach(a => ArtistDtos.Add(new ArtistDto()
            {
                ArtistlID = a.ArtistlID,
                Fname = a.Fname,
                Lname = a.Lname
            }));

            return Ok(ArtistDtos);
        }

        // GET: api/ArtistsData/FindArtist/5
        [ResponseType(typeof(ArtistDto))]
        [HttpGet]
        public IHttpActionResult FindArtist(int id)
        {
            Artist Artist = db.Artists.Find(id);
            ArtistDto ArtistDto = new ArtistDto()
            {
                ArtistlID = Artist.ArtistlID,
                Fname = Artist.Fname,
                Lname = Artist.Lname
            };

            if (Artist == null)
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
            Debug.WriteLine("ha have reached the update artist method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistlID)
            {
                Debug.WriteLine("id mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + artist.ArtistlID);
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
                    Debug.WriteLine("Artist not found");
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

        // POST: api/ArtistsData/AddArtist
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult AddArtist(Artist Artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artists.Add(Artist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Artist.ArtistlID }, Artist);
        }

        // DELETE: api/ArtistsData/DeleteArtist/5
        [ResponseType(typeof(Artist))]
        [HttpPost]
        public IHttpActionResult DeleteArtist(int id)
        {
            Artist Artist = db.Artists.Find(id);
            if (Artist == null)
            {
                return NotFound();
            }

            db.Artists.Remove(Artist);
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