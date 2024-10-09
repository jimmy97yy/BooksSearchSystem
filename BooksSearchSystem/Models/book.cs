using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksSearchSystem.Models
{
    public class Book
    {
        [Key] // 确保这里有 Key 特性
        public string Isbn { get; set; } // 主键

        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        // 每本书可以有多个 Review
        public ICollection<Review> Reviews { get; set; } // 注意这里是 Reviews 而不是 Review
    }
}
