using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class BooksController : ControllerBase
    {
        private IRepository<User> repository;

        public BooksController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        // GET: api/books
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(GetAuthorizedUser().Books);
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var user = GetAuthorizedUser();
            var book = user.Books.FirstOrDefault(book => book.Id == id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound(id);
        }

        // GET api/books/genre
        [HttpGet("genre/{genre}")]
        public IActionResult GetByGenre(string genre)
        {
            var user = GetAuthorizedUser();

            var books = user.Books
                .Where(book => book.Genres.Contains(genre));

            return Ok(books);
        }

        // POST api/books
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var user = GetAuthorizedUser();

            user.Books.Add(book);
            repository.Update(user);

            string url = Url.Action().ToLower() + "/" + book.Id;
            return Created(url, book);
        }

        // PUT api/books
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            var user = GetAuthorizedUser();
            int index = user.Books.FindIndex(old => old.Id == book.Id);

            if (index >= 0)
            {
                user.Books[index] = book;
                repository.Update(user);
            }

            return NoContent();
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = GetAuthorizedUser();
            var bookToDelete = user.Books.FirstOrDefault(book => book.Id == id);

            if (bookToDelete != null)
            {
                user.Books.Remove(bookToDelete);
                repository.Update(user);
            }

            return NoContent();
        }

        private User GetAuthorizedUser()
        {
            string login = User.Identity.Name;

            var user = repository
                .GetAll()
                .FirstOrDefault(user => user.Auth.Login == login);

            return user;
        }
    }
}
