using System.Collections.Generic;
using System.Linq;
using Diary.Business.Services.Abstractions;
using Diary.Data.Context;
using Diary.Data.Entities;

namespace Diary.Business.Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private ApplicationDatabaseContext DatabaseContext { get; set; }

        protected Repository(ApplicationDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        
        public T Get(int id)
        {
            return DatabaseContext.Set<T>().Find(id);
        }

        public T Get(T entity)
        {
            return DatabaseContext.Set<T>().First(e => e.Id == entity.Id);
        }
        public IEnumerable<T> Get(IEnumerable<int> entitiesId)
        {
            return entitiesId.Select(i => DatabaseContext.Set<T>().Find(i)).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return DatabaseContext.Set<T>();
        }

        public void Insert(T entity)
        {
            DatabaseContext.Set<T>().Add(entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            DatabaseContext.Set<T>().AddRange(entities);
        }
        
        public void Update(T entity)
        {
            DatabaseContext.Set<T>().Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            DatabaseContext.Set<T>().UpdateRange(entities);
        }

        public void UpdateById(int id, T entity)
        {
            var ent = Get(id);
            DatabaseContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = Get(id);
            DatabaseContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(int[] entitiesId)
        {
            var entities = Get(entitiesId);
            DatabaseContext.Set<T>().RemoveRange(entities);
        }
    }
}