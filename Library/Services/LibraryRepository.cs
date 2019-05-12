using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Entities;

namespace Library.Services
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public void AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            _context.Authors.Add(author);
            
            //the repository fill the ids (instead of using identity Column)
            if(author.Books.Any())
            {
                foreach(var book in author.Books)
                {
                    book.Id = Guid.NewGuid();
                }
            }

        }

        public void AddBookForAuthor(Guid AuthorId, Book book)
        {
            var author = GetAuthor(AuthorId);
            if (author != null)
            {
                //if there is'nt an Id filled out we should generate one
                if(book.Id == Guid.Empty)
                {
                    book.Id = Guid.NewGuid();
                }
                author.Books.Add(book);                
            }
        }

        public bool AuthorExists(Guid authorId)
        {
            return _context.Authors.Any(x => x.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }       

        public Author GetAuthor(Guid AuthorId)
        {
            return _context.Authors.FirstOrDefault(x => x.Id == AuthorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public Book GetBookForAuthor(Guid BookId, Guid AuthorId)
        {
            return _context.Books.Where(b => b.AuthorId == AuthorId && b.Id == BookId).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooksForAuthor(Guid AuthorId)
        {
            return _context.Books.Where(b => b.AuthorId == AuthorId).OrderBy(b => b.Title).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookForAuthor(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
