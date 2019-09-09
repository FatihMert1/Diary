using Diary.Business.UOW;
using Diary.Data.Context;
using Diary.Data.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Diary.Tests.Integration.UnitOfWorkTests
{
    public class UnitOfWorkTest
    {
        private ApplicationDatabaseContext Context = null;
        private IUnitOfWork UnitOfWork;

        public UnitOfWorkTest()
        {
            var options = new DbContextOptionsBuilder();
            options.UseInMemoryDatabase("unit_of_work_test");
            Context = new ApplicationDatabaseContext(options.Options);
            UnitOfWork = new UnitOfWork(Context);
        }

        [Fact]
        public void Train()
        {
            UnitOfWork.UserRepository.Insert(DummyDatas.GetUser());
            UnitOfWork.SaveChanges();

            var user = UnitOfWork.UserRepository.Get(1);

            user.Should().NotBeNull();
            user.Name.Should().Be(DummyDatas.GetUser().Name);
            user.Diaries.Should().BeEmpty();
            user.Id.Should().Be(1);
        }

        [Fact]
        public void UserRepo_ShouldRemoveById()
        {
            UnitOfWork.UserRepository.Insert(DummyDatas.GetUser());
            UnitOfWork.SaveChanges();
            var user = UnitOfWork.UserRepository.Get(DummyDatas.GetUser());
            UnitOfWork.UserRepository.Delete(user.Id);
            UnitOfWork.SaveChanges();
            
            UnitOfWork.UserRepository.GetAll().Should().BeEmpty();
        }

}
}