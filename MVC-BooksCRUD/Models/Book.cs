using System.ComponentModel.DataAnnotations;

namespace MVC_BooksCRUD.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık boş olamaz")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yazar boş olamaz")]
        [StringLength(100, ErrorMessage = "Yazar en fazla 100 karakter olabilir")]
        public string Author { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok 0 veya daha büyük olmalıdır")]
        public int Stock { get; set; }
    }
}