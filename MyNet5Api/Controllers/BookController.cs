using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MyNet5Api.Controllers {
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> Booklist = new List<Book>()
        {

            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId =1, // personal growth
                PageCount= 200,
                PublishDate = new System.DateTime(2001,06,12)
            },
            new Book{
                Id = 2,
                Title = "Herland",
                GenreId =2, // Science Fiction
                PageCount= 250,
                PublishDate = new System.DateTime(2010,05,23)
            },
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId =2, // Science Fiction
                PageCount= 540,
                PublishDate = new System.DateTime(2001,12,21)
            }

         };
        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = Booklist.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = Booklist.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }
        [HttpPost]
        // IActionResult dönüyor eğer Başarılı veya başarısız diye seçim yapılacak olursa
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = Booklist.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)

                return BadRequest();
            Booklist.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = Booklist.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            return Ok();
        }
    
    }

}