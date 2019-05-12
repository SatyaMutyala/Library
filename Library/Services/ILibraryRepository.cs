using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
    public interface ILibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid AuthorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Book> GetBooksForAuthor(Guid AuthorId);
        Book GetBookForAuthor(Guid BookId, Guid AuthorId);
        void AddBookForAuthor(Guid AuthorId, Book book);

        void UpdateBookForAuthor(Book book);
        void DeleteBook(Book book);
        bool Save();

    }
}
