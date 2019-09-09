using System.Threading.Tasks;
using Diary.Business.Services;
using Diary.Business.Services.Abstractions;
using Diary.Data.Context;

namespace Diary.Business.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; set; }
        public IDiaryRepository DiaryRepository { get; set; }
        private readonly ApplicationDatabaseContext _context;
        
        public UnitOfWork(ApplicationDatabaseContext databaseContext)
        {
            _context = databaseContext;
            UserRepository = new UserRepository(databaseContext);
            DiaryRepository = new DiaryRepository(databaseContext);
        }
        
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        { 
            await _context.SaveChangesAsync();
        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}