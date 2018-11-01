using ProjetoANFService.Infrastructure.Contracts;
using ProjetoANFService.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjetoANFService.Controllers
{
    /// <summary>
    /// Handle CRUD operations for book entities from/to the repository. Inherits from the ApiController class. 
    /// </summary>
    [RoutePrefix("api/Book")]
    public class BookController : ApiController
    {
        #region Attributes

        private readonly IRepository _repository;

        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        public BookController(IRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Actions

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBooks", Name = "GetAllBooksAsync")]
        public async Task<IHttpActionResult> GetAllBooksAsync()
        {
            var books = await _repository.GetAllAsync<Book>();

            return Ok(books);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookById/{id:int}", Name = "GetBookByIdAsync")]
        public async Task<IHttpActionResult> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync<Book>(id);

            return Ok(book);
        }

        #endregion

        #region Post

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateBook", Name = "CreateBookAsync")]
        public async Task<IHttpActionResult> CreateBookAsync([FromBody]Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _repository.GetById<Author>(book.AuthorId);
            var genre = _repository.GetById<Genre>(book.GenreId);

            book.Genre = genre;
            book.Author = author;

            try
            {
                _repository.Create(book);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return CreatedAtRoute("CreateBook", new { book.Id }, book);
        }
        #endregion

        #region Put

        [HttpPut]
        [Route("Updatebook/{id:int}", Name = "UpdatebookAsync")]
        public async Task<IHttpActionResult> UpdateBook([FromBody]Book updatedBook, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedBook.Id != id)
            {
                return NotFound();
            }

            var author = _repository.GetById<Author>(updatedBook.AuthorId);
            var genre = _repository.GetById<Genre>(updatedBook.GenreId);

            updatedBook.Genre = genre;
            updatedBook.Author = author;

            try
            {
                _repository.Update(updatedBook);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("DeleteBook/{id:int}", Name = "DeleteBookAsync")]
        public async Task<IHttpActionResult> DeletebookAsync([FromBody]Book book, int id)
        {
            try
            {
                _repository.Delete(book);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }

        #endregion

        #endregion

        #region Methods

        #region Private

        #endregion

        #region Public
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}