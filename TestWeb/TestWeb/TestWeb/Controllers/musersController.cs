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
using Newtonsoft.Json.Linq;
using TestWeb.Tools;
using TestWeb.Service;
using TestWeb.Enumeration;
using System.Runtime.InteropServices;

namespace TestWeb.Controllers
{
    public class musersController : ApiController
    {

        private dotnetEntities2 db = new dotnetEntities2();

        // GET: api/musers
        public IQueryable<muser> Getmuser()
        {
            return db.muser;
        }

       

        // GET: api/musers/5
        [ResponseType(typeof(muser))]
        public IHttpActionResult Getmuser(int id)
        {
            muser muser = db.muser.Find(id);
            if (muser == null)
            {
                return NotFound();
            }

            return Ok(muser);
        }

        // PUT: api/musers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmuser(int id, muser muser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != muser.userId)
            {
                return BadRequest();
            }

            db.Entry(muser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!muserExists(id))
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


        [ResponseType(typeof(muser))]
        public HttpResponseMessage Login([FromBody] JObject body )
        {
            JObject res = new JObject();
            res.Add("user",null);
            res.Add("result", "requset wrong");
            ServiceMessage message;
            if(body.Property("userName") == null || body.Property("password") == null)
            {
                return ResponseJson.toJson(res);
            }
            else
            {
                muser user = LoginService.Instance.CheckLogin((string)body["userName"], (string)body["password"], out message);
                res["result"] = message.ToString();
            }
        }
        // POST: api/musers
        [ResponseType(typeof(muser))]
        public IHttpActionResult Postmuser(muser muser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.muser.Add(muser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (muserExists(muser.userId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = muser.userId }, muser);
        }

        // DELETE: api/musers/5
        [ResponseType(typeof(muser))]
        public IHttpActionResult Deletemuser(int id)
        {
            muser muser = db.muser.Find(id);
            if (muser == null)
            {
                return NotFound();
            }

            db.muser.Remove(muser);
            db.SaveChanges();

            return Ok(muser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool muserExists(int id)
        {
            return db.muser.Count(e => e.userId == id) > 0;
        }
    }
}