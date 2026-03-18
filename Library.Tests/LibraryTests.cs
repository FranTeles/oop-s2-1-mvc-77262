using Library.Domain;
using System;
using Xunit;

namespace Library.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void Book_Should_Be_Available_When_Created()
        {
            var book = new Book
            {
                Title = "1984",
                Author = "George Orwell",
                Isbn = "9780451524935",
                Category = "Sci-Fi",
                IsAvailable = true
            };

            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Member_Should_Store_Email_Correctly()
        {
            var member = new Member
            {
                FullName = "Ana Silva",
                Email = "ana@email.com",
                Phone = "123456789"
            };

            Assert.Equal("ana@email.com", member.Email);
        }

        [Fact]
        public void Active_Loan_Should_Make_Book_Unavailable()
        {
            var book = new Book
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Isbn = "9780261102217",
                Category = "Fantasy",
                IsAvailable = true
            };

            var loan = new Loan
            {
                BookId = 1,
                MemberId = 1,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                ReturnedDate = null
            };

            if (loan.ReturnedDate == null)
            {
                book.IsAvailable = false;
            }

            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void Returned_Loan_Should_Allow_Book_To_Be_Available()
        {
            var book = new Book
            {
                Title = "Dracula",
                Author = "Bram Stoker",
                Isbn = "9780141439846",
                Category = "Horror",
                IsAvailable = false
            };

            var loan = new Loan
            {
                BookId = 1,
                MemberId = 1,
                LoanDate = DateTime.Now.AddDays(-10),
                DueDate = DateTime.Now.AddDays(-3),
                ReturnedDate = DateTime.Now
            };

            if (loan.ReturnedDate != null)
            {
                book.IsAvailable = true;
            }

            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Loan_Should_Be_Overdue_When_DueDate_Has_Passed_And_Not_Returned()
        {
            var loan = new Loan
            {
                BookId = 1,
                MemberId = 1,
                LoanDate = DateTime.Now.AddDays(-10),
                DueDate = DateTime.Now.AddDays(-1),
                ReturnedDate = null
            };

            var isOverdue = loan.DueDate < DateTime.Now && loan.ReturnedDate == null;

            Assert.True(isOverdue);
        }
    }
}
