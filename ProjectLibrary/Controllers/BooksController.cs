using Microsoft.AspNetCore.Mvc;
using ProjectLibrary.Data.Interfaces;
using ProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private IBooksRepository repository;

        public BooksController(IBooksRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/books
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        { 
            var book = repository.Get(id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound(id);
        }

        // GET api/books/genre
        [HttpGet("genre/{genre}")]
        public IActionResult Get(string genre)
        {
            var books = repository
                .GetAll()
                .Where(book => book.Genres.Contains(genre));

            return Ok(books);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var created = repository.Create(book);

            if (created != null)
            {
                string url = Url.Action().ToLower() + "/" + created.Id;
                return Created(url, created);
            }

            return Ok(book);
        }

        // PUT api/books
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            repository.Update(book);

            // always return NoContent, cause PUT is idempotent
            return NoContent();
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);

            // always return NoContent, cause DELETE is idempotent
            return NoContent();
        }
    }
}
