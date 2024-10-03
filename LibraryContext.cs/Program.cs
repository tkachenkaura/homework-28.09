using System;
using System.Linq;
using Library;

namespace LibraryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {
                Console.WriteLine("Вітаємо у бібліотечній системі!");

                bool isAuthenticated = false;
                Librarian currentLibrarian = null;
                Reader currentReader = null;

                while (!isAuthenticated)
                {
                    Console.WriteLine("Введіть логін:");
                    string login = Console.ReadLine();

                    Console.WriteLine("Введіть пароль:");
                    string password = Console.ReadLine();

                   
                    currentLibrarian = context.Librarians
                        .FirstOrDefault(l => l.Login == login && l.Password == password);

                    
                    currentReader = context.Readers
                        .FirstOrDefault(r => r.Login == login && r.Password == password);

                    if (currentLibrarian != null)
                    {
                        Console.WriteLine($"Вітаємо, {currentLibrarian.Login}! Ви увійшли як бібліотекар.");
                        isAuthenticated = true;
                        LibrarianMenu(context, currentLibrarian);
                    }
                    else if (currentReader != null)
                    {
                        Console.WriteLine($"Вітаємо, {currentReader.FirstName}! Ви увійшли як читач.");
                        isAuthenticated = true;
                        ReaderMenu(context, currentReader);
                    }
                    else
                    {
                        Console.WriteLine("Невірний логін або пароль. Спробуйте ще раз.");
                    }
                }
            }
        }

        static void LibrarianMenu(LibraryContext context, Librarian librarian)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n1. Переглянути книги");
                Console.WriteLine("2. Додати книгу");
                Console.WriteLine("3. Додати читача");
                Console.WriteLine("4. Вийти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewBooks(context);
                        break;
                    case "2":
                        AddBook(context);
                        break;
                    case "3":
                        RegisterReader(context);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void ReaderMenu(LibraryContext context, Reader reader)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n1. Переглянути книги");
                Console.WriteLine("2. Взяти книгу");
                Console.WriteLine("3. Вийти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewBooks(context);
                        break;
                    case "2":
                        BorrowBook(context, reader);
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void ViewBooks(LibraryContext context)
        {
            var books = context.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}: {book.Title} ({book.PublicationYear})");
            }
        }

        static void AddBook(LibraryContext context)
        {
            Console.WriteLine("Введіть назву книги:");
            string title = Console.ReadLine();

            Console.WriteLine("Введіть рік публікації:");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть країну:");
            string country = Console.ReadLine();

            Console.WriteLine("Введіть місто:");
            string city = Console.ReadLine();

            Book book = new Book
            {
                Title = title,
                PublicationYear = year,
                Country = country,
                City = city
            };

            context.Books.Add(book);
            context.SaveChanges();

            Console.WriteLine("Книгу додано успішно!");
        }

        static void RegisterReader(LibraryContext context)
        {
            Console.WriteLine("Введіть логін читача:");
            string login = Console.ReadLine();

            Console.WriteLine("Введіть пароль:");
            string password = Console.ReadLine();

            Console.WriteLine("Введіть ім'я:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введіть прізвище:");
            string lastName = Console.ReadLine();

            Reader reader = new Reader
            {
                Login = login,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };

            context.Readers.Add(reader);
            context.SaveChanges();

            Console.WriteLine("Читача успішно зареєстровано!");
        }

        static void BorrowBook(LibraryContext context, Reader reader)
        {
            Console.WriteLine("Введіть ID книги, яку хочете взяти:");
            int bookId = int.Parse(Console.ReadLine());

            var book = context.Books.Find(bookId);
            if (book != null)
            {
                BorrowedBook borrowedBook = new BorrowedBook
                {
                    BookId = book.Id,
                    ReaderId = reader.Id,
                    BorrowedAt = DateTime.Now,
                    ReturnDate = DateTime.Now.AddDays(30)
                };

                context.BorrowedBooks.Add(borrowedBook);
                context.SaveChanges();

                Console.WriteLine($"Книга '{book.Title}' успішно взята.");
            }
            else
            {
                Console.WriteLine("Книга не знайдена.");
            }
        }
    }
}