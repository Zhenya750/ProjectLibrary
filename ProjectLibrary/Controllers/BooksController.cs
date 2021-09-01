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
        public JsonResult Get()
        {
            return new JsonResult(repository.GetAll());
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        { 
            var book = repository.Get(id);

            if (book != null)
            {
                return new JsonResult(book);
            }

            return new JsonResult($"Book with id {id} was not found");
        }

        // GET api/books/genre
        [HttpGet("genre/{genre}")]
        public JsonResult Get(string genre)
        {
            return new JsonResult(repository
                .GetAll()
                .Where(book => book.Genres.Contains(genre)));
        }

        // POST api/books
        [HttpPost]
        public JsonResult Post([FromBody] Book book)
        {
            repository.Create(book);
            return new JsonResult($"book {book} was created");
        }

        // PUT api/books
        [HttpPut]
        public JsonResult Put([FromBody] Book book)
        {
            var oldBook = repository.Get(book.Id);

            if (oldBook != null)
            {
                repository.Update(book);
                return new JsonResult($"book was updated to {book}");
            }

            return new JsonResult($"can't update book to {book}");
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            repository.Delete(id);
            return new JsonResult($"book with id {id} was deleted");
        }
    }
}
