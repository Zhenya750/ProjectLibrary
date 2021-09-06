using ProjectLibrary.Data.Interfaces;
using ProjectLibrary.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLibrary.Data
{
    public class MemoryRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly List<T> models;

        public MemoryRepository()
        {
            models = new List<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return models;
        }

        public T Get(string id)
        {
            return models.Find(model => model.Id == id);
        }

        public T Create(T model)
        {
            var existed = models.FirstOrDefault(existed => existed.Id == model.Id);

            if (existed != null)
            {
                return null;
            }

            models.Add(model);
            return model;
        }

        public void Update(T model)
        {
            int index = models.FindIndex(old => old.Id == model.Id);

            if (index >= 0)
            {
                models[index] = model;
            }
        }

        public void Delete(string id)
        {
            var model = Get(id);

            if (model != null)
            {
                models.Remove(model);
            }
        }
    }
}
