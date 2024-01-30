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
    public class ShopController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Shop
        public IQueryable<shop> Getshops()
        {
            return db.shops;
        }

        // GET: api/Shop/5
        [ResponseType(typeof(shop))]
        public IHttpActionResult Getshop(string id)
        {
            shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            return Ok(shop);
        }

        // PUT: api/Shop/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putshop(string id, shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != shop.username)
            {
                return BadRequest();
            }


            db.Entry(shop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!shopExists(id))
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

        // POST: api/Shop
        [ResponseType(typeof(shop))]
        public IHttpActionResult Postshop(shop shop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.shops.Add(shop);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (shopExists(shop.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shop.username }, shop);
        }

        // DELETE: api/Shop/5
        [ResponseType(typeof(shop))]
        public IHttpActionResult Deleteshop(string id)
        {
            shop shop = db.shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }

            db.shops.Remove(shop);
            db.SaveChanges();

            return Ok(shop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool shopExists(string id)
        {
            return db.shops.Count(e => e.username == id) > 0;
        }
    }
}