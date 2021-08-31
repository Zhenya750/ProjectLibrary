using ProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Data.Interfaces
{
    public interface IBooksService
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetByGenre(string genre);
        void Add(Book book);
        void Delete(Book book);
    }
}
