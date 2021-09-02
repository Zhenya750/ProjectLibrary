using ProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLibrary.Data.Interfaces
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll();
        Book Get(int id);
        Book Create(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
