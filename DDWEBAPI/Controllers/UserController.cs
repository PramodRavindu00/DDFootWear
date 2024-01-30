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
using DDWEBAPI.Models;

namespace DDWEBAPI.Controllers
{
    public class UserController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/User
        public IQueryable<UserTable> GetUserTables()
        {
            return db.UserTables;
        }

        // GET: api/User/5
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult GetUserTable(string id)
        {
            UserTable userTable = db.UserTables.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            return Ok(userTable);
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserTable(string id, UserTable userTable)
        {
            

            if (id != userTable.username)
            {
                return BadRequest();
            }

            db.Entry(userTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTableExists(id))
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

        // POST: api/User
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult PostUserTable(UserTable userTable)
        {
           

            db.UserTables.Add(userTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserTableExists(userTable.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userTable.username }, userTable);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(UserTable))]
        public IHttpActionResult DeleteUserTable(string id)
        {
            UserTable userTable = db.UserTables.Find(id);
            if (userTable == null)
            {
                return NotFound();
            }

            db.UserTables.Remove(userTable);
            db.SaveChanges();

            return Ok(userTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTableExists(string id)
        {
            return db.UserTables.Count(e => e.username == id) > 0;
        }
    }
}