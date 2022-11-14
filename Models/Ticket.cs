using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7.Shared.Models
{
    public class Ticket
    {
        [Key]
        public int ID { get; set; }
        public int ReaderID { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
      
        public Reader Reader { get; set; } = null!;
    }
}
