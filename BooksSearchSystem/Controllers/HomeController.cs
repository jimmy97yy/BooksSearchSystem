using BooksSearchSystem.Data;
using BooksSearchSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplicationTest;

namespace BooksSearchSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly BooksCrawler _booksInfo = new BooksCrawler();
        private readonly ApplicationDbContext _context; // 注入資料庫上下文

        public HomeController(ApplicationDbContext context)
        {
            _context = context; // 注入資料庫上下文
        }

        public async Task<IActionResult> Index()
        {
            string bookId = "0010764130"; // 書籍 ID
            var book = await _booksInfo.GetBookInfo(bookId); // 爬取書籍資料

            // 確認書籍是否已存在於資料庫，避免重複插入
            var existingBook = await _context.Books.FindAsync(book.Isbn);
            if (existingBook == null)
            {
                // 如果資料庫中不存在，將書籍資料存入資料庫
                _context.Books.Add(book);
                await _context.SaveChangesAsync(); // 保存變更到資料庫
            }

            // 顯示書籍資料
            ViewBag.Book = book;
            return View();
        }
    }
}
