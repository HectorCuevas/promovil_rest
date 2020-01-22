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
using promovil_rest.Models;

namespace promovil_rest.Controllers
{
    public class ItemsController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/Items
        public IQueryable<Items> GetItems()
        {
            return db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Items))]
        public IHttpActionResult GetItems(int id)
        {
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItems(int id, Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items.id)
            {
                return BadRequest();
            }

            db.Entry(items).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        // POST: api/Items
        [ResponseType(typeof(Items))]
        public String PostItems(Items items)
        {
            String res;
         //   res = String.Join(",", items);
            return items.ToString();
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Items))]
        public IHttpActionResult DeleteItems(int id)
        {
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return NotFound();
            }

            db.Items.Remove(items);
            db.SaveChanges();

            return Ok(items);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemsExists(int id)
        {
            return db.Items.Count(e => e.id == id) > 0;
        }
    }
}