using Library.API.Models;
using Library.API.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;        

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var allAuthors = _libraryRepository.GetAuthors();
            var authors = Mapper.Map<IEnumerable<AuthorDetails>>(allAuthors);
            
            return new JsonResult(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id)
        {
            var authorFromRepo = _libraryRepository.GetAuthor(id);
            var author = Mapper.Map<AuthorDetails>(authorFromRepo);
            return new JsonResult(author);
        }
    }
}
