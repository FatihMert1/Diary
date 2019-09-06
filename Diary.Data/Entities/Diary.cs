namespace Diary.Data.Entities
{
    public class Diary : BaseEntity
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Diary()
        {
            
        }
    }
}