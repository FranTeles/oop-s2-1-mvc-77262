using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain
{
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Required]
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
