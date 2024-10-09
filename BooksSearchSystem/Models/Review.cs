using System;

namespace BooksSearchSystem.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Isbn { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Content { get; set; }

        // 外鍵關聯
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
