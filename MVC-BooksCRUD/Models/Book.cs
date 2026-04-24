using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_BooksCRUD.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık boş olamaz")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yazar boş olamaz")]
        [StringLength(100)]
        public string Author { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kategori seçmek zorunlu")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}