using System.Collections.Generic;
using System.Linq;

namespace MVC_BooksCRUD.Models
{
    public class BookRepository
    {
      
        private static List<Book> books = new List<Book>()
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", Price = 50, Stock = 10 },
            new Book { Id = 2, Title = "Suç ve Ceza", Author = "Dostoyevski", Price = 60, Stock = 5 }
        };

   
        public List<Book> GetAll()
        {
            return books;
        }

        public void Add(Book newBook)
        {
            if (books.Count == 0)
                newBook.Id = 1;
            else
                newBook.Id = books.Max(x => x.Id) + 1;

            books.Add(newBook);
        }

     
        public void Remove(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
        }

   
        public void Update(Book updatedBook)
        {
            var book = books.FirstOrDefault(x => x.Id == updatedBook.Id);

            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.Price = updatedBook.Price;
                book.Stock = updatedBook.Stock;
            }
        }
    }
}