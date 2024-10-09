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
        private readonly ApplicationDbContext _context; // �`�J��Ʈw�W�U��

        public HomeController(ApplicationDbContext context)
        {
            _context = context; // �`�J��Ʈw�W�U��
        }

        public async Task<IActionResult> Index()
        {
            string bookId = "0010764130"; // ���y ID
            var book = await _booksInfo.GetBookInfo(bookId); // �������y���

            // �T�{���y�O�_�w�s�b���Ʈw�A�קK���ƴ��J
            var existingBook = await _context.Books.FindAsync(book.Isbn);
            if (existingBook == null)
            {
                // �p�G��Ʈw�����s�b�A�N���y��Ʀs�J��Ʈw
                _context.Books.Add(book);
                await _context.SaveChangesAsync(); // �O�s�ܧ���Ʈw
            }

            // ��ܮ��y���
            ViewBag.Book = book;
            return View();
        }
    }
}
