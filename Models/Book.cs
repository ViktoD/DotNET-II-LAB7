using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7.Shared.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        public int Price { get; set; }
        public int YearPublished { get; set; }
    }
}
