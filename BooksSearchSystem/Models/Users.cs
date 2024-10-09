using System.Collections.Generic;

namespace BooksSearchSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        // 每個 User 可以有多個 Review
        public ICollection<Review> Reviews { get; set; }
    }
}
