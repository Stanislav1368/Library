using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook([FromForm] CreateBookDto createBookDto)
        {
            var createdBook = await _bookService.CreateBookAsync(createBookDto);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("{id}")] //Доработать
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, bookDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/addComment")] 
        public async Task<IActionResult> CommentOnTheBook(int id, CreateCommentDto createCommentDto)
        {
            try
            {
                await _bookService.CommentOnTheBookAsync(id, createCommentDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/addRating")]
        public async Task<IActionResult> RateBook(int id, AddRatingDto addRatingDto)
        {
            try
            {
                await _bookService.AddRatingAsync(id, addRatingDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}