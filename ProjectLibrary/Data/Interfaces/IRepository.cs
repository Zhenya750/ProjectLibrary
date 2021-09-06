using ProjectLibrary.Models.Base;
using System.Collections.Generic;

namespace ProjectLibrary.Data.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        T Create(T model);
        void Update(T model);
        void Delete(string id);
    }
}
