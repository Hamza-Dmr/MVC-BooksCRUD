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

    public IActionResult Index(string search, int? categoryId, string sortOrder)
    {
        var books = _context.Books
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            books = books.Where(x => x.Title.Contains(search));
        }

        if (categoryId.HasValue && categoryId > 0)
        {
            books = books.Where(x => x.CategoryId == categoryId);
        }

        switch (sortOrder)
        {
            case "title_desc":
                books = books.OrderByDescending(x => x.Title);
                break;

            case "price_asc":
                books = books.OrderBy(x => x.Price);
                break;

            case "price_desc":
                books = books.OrderByDescending(x => x.Price);
                break;

            case "stock_asc":
                books = books.OrderBy(x => x.Stock);
                break;

            case "stock_desc":
                books = books.OrderByDescending(x => x.Stock);
                break;

            default:
                books = books.OrderBy(x => x.Title);
                break;
        }

        ViewBag.Categories = _context.Categories.ToList();

        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.SortOrder = sortOrder;

        return View(books.ToList());
    }

    public IActionResult Add()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Book newBook)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(newBook);
        }

        _context.Books.Add(newBook);
        _context.SaveChanges();

        TempData["Message"] = "Kitap eklendi";

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