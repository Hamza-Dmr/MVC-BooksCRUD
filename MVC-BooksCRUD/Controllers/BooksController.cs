using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_BooksCRUD.Models;

public class BooksController : Controller
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    
    public IActionResult Index()
    {
        var books = _context.Books
            .Include(x => x.Category)
            .ToList();

        return View(books);
    }

    
    public IActionResult Add()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    
    [HttpPost]
    public IActionResult Add(Book newBook)
    {
        Console.WriteLine("CATEGORY ID: " + newBook.CategoryId);

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(newBook);
        }

        _context.Books.Add(newBook);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
 
    public IActionResult Update(int id)
    {
        var book = _context.Books.Find(id);

        if (book == null)
            return NotFound();

        ViewBag.Categories = _context.Categories.ToList();

        return View(book);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Book updatedBook)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList(); 
            return View(updatedBook);
        }

        var book = _context.Books.Find(updatedBook.Id);

        if (book == null)
            return NotFound();

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Price = updatedBook.Price;
        book.Stock = updatedBook.Stock;
        book.CategoryId = updatedBook.CategoryId;

        _context.SaveChanges();

        TempData["Message"] = "Kitap güncellendi";

        return RedirectToAction("Index");
    }

   
    public IActionResult Remove(int id)
    {
        var book = _context.Books.Find(id);

        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        TempData["Message"] = "Kitap silindi";

        return RedirectToAction("Index");
    }
}