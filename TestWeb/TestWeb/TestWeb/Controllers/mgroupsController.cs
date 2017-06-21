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
    public class mgroupsController : ApiController
    {
        private dotnetEntities2 db = new dotnetEntities2();

        // GET: api/mgroups
        public IQueryable<mgroup> Getmgroup()
        {
            return db.mgroup;
        }

        // GET: api/mgroups/5
        [ResponseType(typeof(mgroup))]
        public IHttpActionResult Getmgroup(int id)
        {
            mgroup mgroup = db.mgroup.Find(id);
            if (mgroup == null)
            {
                return NotFound();
            }

            return Ok(mgroup);
        }

        // PUT: api/mgroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmgroup(int id, mgroup mgroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mgroup.groupId)
            {
                return BadRequest();
            }

            db.Entry(mgroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mgroupExists(id))
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

        // POST: api/mgroups
        [ResponseType(typeof(mgroup))]
        public IHttpActionResult Postmgroup(mgroup mgroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.mgroup.Add(mgroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (mgroupExists(mgroup.groupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = mgroup.groupId }, mgroup);
        }

        // DELETE: api/mgroups/5
        [ResponseType(typeof(mgroup))]
        public IHttpActionResult Deletemgroup(int id)
        {
            mgroup mgroup = db.mgroup.Find(id);
            if (mgroup == null)
            {
                return NotFound();
            }

            db.mgroup.Remove(mgroup);
            db.SaveChanges();

            return Ok(mgroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool mgroupExists(int id)
        {
            return db.mgroup.Count(e => e.groupId == id) > 0;
        }
    }
}