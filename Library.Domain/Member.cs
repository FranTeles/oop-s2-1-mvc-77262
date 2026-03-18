using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
