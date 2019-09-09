using Diary.Business.Services.Abstractions;
using Diary.Data.Context;

namespace Diary.Business.Services
{
    public class DiaryRepository : Repository<Data.Entities.Diary>, IDiaryRepository
    {
        protected readonly ApplicationDatabaseContext DatabaseContext;
        
        public DiaryRepository(ApplicationDatabaseContext databaseContext) : base(databaseContext)
        {
            DatabaseContext = databaseContext;
        }
        
    }
}