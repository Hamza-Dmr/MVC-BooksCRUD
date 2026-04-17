using Microsoft.AspNetCore.Mvc;
using MVC_BooksCRUD.Models;
using System.Linq;

namespace MVC_BooksCRUD.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

    
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Book newBook)
        {
         
            if (!ModelState.IsValid)
            {
                return View(newBook);
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();

   
            TempData["Success"] = "Kitap başarıyla eklendi";

            return RedirectToAction("Index");
        }

     
        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

         
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book updatedBook)
        {
    
            if (!ModelState.IsValid)
            {
                return View(updatedBook);
            }

            var book = _context.Books.Find(updatedBook.Id);


            if (book == null)
            {
                return NotFound();
            }

     
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;
            book.Stock = updatedBook.Stock;

            _context.SaveChanges();

            TempData["Success"] = "Kitap başarıyla güncellendi";

            return RedirectToAction("Index");
        }

      
        public IActionResult Remove(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            TempData["Success"] = "Kitap silindi";

            return RedirectToAction("Index");
        }
    }
}