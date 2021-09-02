using ProjectLibrary.Data.Interfaces;
using ProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Data
{
    public class BooksMemoryRepository : IBooksRepository
    {
        private List<Book> books;

        public BooksMemoryRepository()
        {
            books = new List<Book>
            {
                new Book { Id = 1, Name = "book1", Author = "author1", Genres = new[] { "g1", "g2" } },
                new Book { Id = 2, Name = "book2", Author = "author2", Genres = new[] { "g1" } },
                new Book { Id = 3, Name = "book3", Author = "author3", Genres = new[] { "g3" } },
            };
        }

        public IEnumerable<Book> GetAll()
        {
            return books;
        }

        public Book Get(int id)
        {
            return books.Find(book => book.Id == id);
        }

        public Book Create(Book book)
        {
            int index = books.FindIndex(addedBook => addedBook.Id == book.Id);

            if (index >= 0)
            {
                return null;
            }

            books.Add(book);
            return book;
        }

        public void Update(Book book)
        {
            int index = books.FindIndex(oldBook => oldBook.Id == book.Id);

            if (index >= 0)
            {
                books[index] = book;
            }
        }

        public void Delete(int id)
        {
            var book = Get(id);

            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}
