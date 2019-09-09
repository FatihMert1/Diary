using Diary.Data.Entities;

namespace Diary.Business.Services.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {

        void Delete(int id);
    }
}