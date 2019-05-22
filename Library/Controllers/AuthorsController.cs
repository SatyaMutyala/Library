using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;
        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet("api/authors")]
        public IActionResult GetAuthors()
        {
            var allAuthors = _libraryRepository.GetAuthors();
            return new JsonResult(allAuthors);
        }
    }
}
