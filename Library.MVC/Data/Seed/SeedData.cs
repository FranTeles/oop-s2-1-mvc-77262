using Bogus;
using Library.Domain;

namespace Library.MVC.Data.Seed
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            var books = new List<Book>
            {
                new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Isbn = "9780261102217", Category = "Fantasy", IsAvailable = true },
                new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Isbn = "9780261102385", Category = "Fantasy", IsAvailable = true },
                new Book { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Isbn = "9780747532699", Category = "Fantasy", IsAvailable = true },
                new Book { Title = "Harry Potter and the Chamber of Secrets", Author = "J.K. Rowling", Isbn = "9780747538493", Category = "Fantasy", IsAvailable = true },
                new Book { Title = "A Game of Thrones", Author = "George R.R. Martin", Isbn = "9780553103540", Category = "Fantasy", IsAvailable = true },

                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Isbn = "9780141439518", Category = "Romance", IsAvailable = true },
                new Book { Title = "Jane Eyre", Author = "Charlotte Bronte", Isbn = "9780141441146", Category = "Romance", IsAvailable = true },
                new Book { Title = "Me Before You", Author = "Jojo Moyes", Isbn = "9780718157839", Category = "Romance", IsAvailable = true },
                new Book { Title = "The Notebook", Author = "Nicholas Sparks", Isbn = "9780446605236", Category = "Romance", IsAvailable = true },
                new Book { Title = "The Fault in Our Stars", Author = "John Green", Isbn = "9780142424179", Category = "Romance", IsAvailable = true },

                new Book { Title = "Dracula", Author = "Bram Stoker", Isbn = "9780141439846", Category = "Horror", IsAvailable = true },
                new Book { Title = "Frankenstein", Author = "Mary Shelley", Isbn = "9780141439471", Category = "Horror", IsAvailable = true },
                new Book { Title = "The Shining", Author = "Stephen King", Isbn = "9780307743657", Category = "Horror", IsAvailable = true },
                new Book { Title = "It", Author = "Stephen King", Isbn = "9781501142970", Category = "Horror", IsAvailable = true },
                new Book { Title = "The Haunting of Hill House", Author = "Shirley Jackson", Isbn = "9780143039983", Category = "Horror", IsAvailable = true },

                new Book { Title = "1984", Author = "George Orwell", Isbn = "9780451524935", Category = "Sci-Fi", IsAvailable = true },
                new Book { Title = "Dune", Author = "Frank Herbert", Isbn = "9780441172719", Category = "Sci-Fi", IsAvailable = true },
                new Book { Title = "The Martian", Author = "Andy Weir", Isbn = "9780553418026", Category = "Sci-Fi", IsAvailable = true },
                new Book { Title = "Fahrenheit 451", Author = "Ray Bradbury", Isbn = "9781451673319", Category = "Sci-Fi", IsAvailable = true },
                new Book { Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Isbn = "9780345391803", Category = "Sci-Fi", IsAvailable = true }
            };

            context.Books.AddRange(books);

            var memberFaker = new Faker<Member>()
                .RuleFor(m => m.FullName, f => f.Name.FullName())
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.Phone, f => f.Phone.PhoneNumber());

            var members = memberFaker.Generate(10);
            context.Members.AddRange(members);

            context.SaveChanges();

            var loans = new List<Loan>();
            var random = new Random();

            var availableBooks = books.ToList();

            for (int i = 0; i < 15 && availableBooks.Any(); i++)
            {
                var book = availableBooks[random.Next(availableBooks.Count)];
                var member = members[random.Next(members.Count)];

                var loanDate = DateTime.Now.AddDays(-random.Next(1, 20));
                var dueDate = loanDate.AddDays(7);

                DateTime? returnedDate = null;

                if (random.Next(2) == 1)
                {
                    returnedDate = dueDate.AddDays(random.Next(-2, 3));
                }

                loans.Add(new Loan
                {
                    BookId = book.Id,
                    MemberId = member.Id,
                    LoanDate = loanDate,
                    DueDate = dueDate,
                    ReturnedDate = returnedDate
                });

                if (returnedDate == null)
                {
                    book.IsAvailable = false;
                }

                availableBooks.Remove(book);
            }

            context.Loans.AddRange(loans);
            context.SaveChanges();
        }
    }
}