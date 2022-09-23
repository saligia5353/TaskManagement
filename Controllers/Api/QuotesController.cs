using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagement.Models;

namespace TaskManagement.Controllers.Api
{
    [Authorize]
    public class QuotesController : ApiController
    {
        private QuoteDBEntities _entities;

        public QuotesController()
        {
            _entities = new QuoteDBEntities();
        }

        // GET /api/quotes
        public IEnumerable<Quote> Get()
        {
            return _entities.Quotes.ToList();
        }

        // GET /api/quotes/1
        public IHttpActionResult Get(int id)
        {
            var quote = _entities.Quotes.SingleOrDefault(q => q.QuoteID == id);

            if (quote == null)
                return Content(HttpStatusCode.NotFound, "Quote does not exist.");

            return Ok(quote);
        }

        // POST /api/quotes
        [HttpPost]
        public IHttpActionResult Create(Quote quote)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _entities.Quotes.Add(quote);
            _entities.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + quote.QuoteID), quote);
        }

        // PUT /api/quotes/1
        [HttpPut]
        public void Update(int id, Quote quote)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var quoteInDb = _entities.Quotes.SingleOrDefault(q => q.QuoteID == id);

            if (quoteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            quoteInDb.QuoteType = quote.QuoteType;
            quoteInDb.Contact = quote.Contact;
            quoteInDb.Task = quote.Task;
            quoteInDb.DueDate = quote.DueDate;
            quoteInDb.TaskType = quote.TaskType;

            _entities.SaveChanges();
        }

        // DELETE /api/quotes/1
        [HttpDelete]
        public void Delete(int id)
        {
            var quoteInDb = _entities.Quotes.SingleOrDefault(q => q.QuoteID == id);

            if (quoteInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _entities.Quotes.Remove(quoteInDb);
            _entities.SaveChanges();
        }
    }
}
