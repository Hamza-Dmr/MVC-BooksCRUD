using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_BooksCRUD.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı boş olamaz")]
        public string Name { get; set; }

     
        public List<Book> Books { get; set; }
    }
}