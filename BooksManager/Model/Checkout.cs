using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Model
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public String PhoneNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
