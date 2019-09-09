using Diary.Business.Services.Abstractions;
using Diary.Data.Context;
using Diary.Data.Entities;

namespace Diary.Business.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDatabaseContext _databaseContext;
        public UserRepository(ApplicationDatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Delete(int id)
        {
            var user =_databaseContext.Users.Find(id);
            _databaseContext.Remove(user);
        }
    }
}