using System.Collections.Generic;
using Diary.Data.Entities;

namespace Diary.Business.Services.Abstractions
{
    public interface IRepository<T> where T :  BaseEntity
    {

        T Get(int id);
        T Get(T entity);
        IEnumerable<T> Get(IEnumerable<int> entitiesId);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(List<T> entities);
        void UpdateById(int id, T entity);
        void Delete(T entity);
        void DeleteById(int id);
        void DeleteRange(int[] entitiesId);
        
    }
}