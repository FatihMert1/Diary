using System;
using Diary.Data.Entities;

namespace Diary.Tests.Integration
{
    public static class DummyDatas
    {
        public static User GetUser()
        {
            return new User()
            {
                Id = 1, Name = "Fatih Rahman", LastName = "Mert", Password = "1234", NickName = "duffyDuct",
                CreatedAt = DateTime.Now
            };
        }
    }
}