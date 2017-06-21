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
using TestWeb.Models;

namespace TestWeb.Controllers
{
    public class mjoinsController : ApiController
    {
        private dotnetEntities2 db = new dotnetEntities2();

        // GET: api/mjoins
        public IQueryable<mjoin> Getmjoin()
        {
            return db.mjoin;
        }

        // GET: api/mjoins/5
        [ResponseType(typeof(mjoin))]
        public IHttpActionResult Getmjoin(int id)
        {
            mjoin mjoin = db.mjoin.Find(id);
            if (mjoin == null)
            {
                return NotFound();
            }

            return Ok(mjoin);
        }

        // PUT: api/mjoins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmjoin(int id, mjoin mjoin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mjoin.userId)
            {
                return BadRequest();
            }

            db.Entry(mjoin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mjoinExists(id))
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

        // POST: api/mjoins
        [ResponseType(typeof(mjoin))]
        public IHttpActionResult Postmjoin(mjoin mjoin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mjoin.Add(mjoin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (mjoinExists(mjoin.userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mjoin.userId }, mjoin);
        }

        // DELETE: api/mjoins/5
        [ResponseType(typeof(mjoin))]
        public IHttpActionResult Deletemjoin(int id)
        {
            mjoin mjoin = db.mjoin.Find(id);
            if (mjoin == null)
            {
                return NotFound();
            }

            db.mjoin.Remove(mjoin);
            db.SaveChanges();

            return Ok(mjoin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mjoinExists(int id)
        {
            return db.mjoin.Count(e => e.userId == id) > 0;
        }
    }
}