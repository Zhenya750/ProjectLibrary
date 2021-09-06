using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProjectLibrary.Data.Interfaces;
using ProjectLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectLibrary.Data
{
    public class MongoRepository<T> : IRepository<T> where T : BaseModel
    {
        private IMongoCollection<T> models;

        public MongoRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDB");

            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(connection.DatabaseName);

            models = db.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetAll()
        {
            return models.Find(FilterDefinition<T>.Empty).ToEnumerable();
        }

        public T Get(string id)
        {
            try
            {
                var model = models.Find(m => m.Id == id).FirstOrDefault();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T Create(T model)
        {
            try
            {
                models.InsertOne(model);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(T model)
        {
            try
            {
                models.ReplaceOne(old => old.Id == model.Id, model);
            }
            catch (Exception) { }
        }

        public void Delete(string id)
        {
            try
            {
                models.DeleteOne(model => model.Id == id);
            }
            catch (Exception) { }
        }
    }
}
