using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7.Shared.Models
{
    public class Reader
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Surname { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string PatronymicName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;

        public int Phone { get; set; }
        public DateTime DateBirth { get; set; }
    }
}
