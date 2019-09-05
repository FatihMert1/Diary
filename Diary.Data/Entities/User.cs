using System.Collections.Generic;

namespace Diary.Data.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }

        
        public ICollection<Diary> Diaries { get; set; }
        
        public User()
        {
            Diaries = new HashSet<Diary>();
        }
    }
}