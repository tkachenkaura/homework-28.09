using System;
using System.Collections.Generic;

namespace Library
{
    public class Librarian
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Reader
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<BorrowedBook> BorrowedBooks { get; set; }
    }

    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CodeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int CodeTypeId { get; set; }
        public int PublicationYear { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public CodeType CodeType { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }

    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

    public class BorrowedBook
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime BorrowedAt { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
    }
}
