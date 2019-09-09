using System;
using System.Threading.Tasks;
using Diary.Business.Services.Abstractions;

namespace Diary.Business.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; set; }
        IDiaryRepository DiaryRepository { get; set; }

        int SaveChanges();
        Task SaveChangesAsync();

        void RollBack();
    }
}