using ProjetoANFCore.Helpers;
using ProjetoANFWeb.Controllers.Base;
using ProjetoANFWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjetoANFWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BookController : BaseController
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Actions

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, "api/Book/GetAllBooks")
                    .WithJsonContent()
                    .GetAsync<IEnumerable<BookViewModel>>();

                return View(response.ResponseBody.ToList() ?? new List<BookViewModel>());
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Title = ex.Message, Message = ex.ToString() });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await GetAuthorsAndGenres(null, null);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Book/GetBookById/{id}")
                    .WithJsonContent()
                    .GetAsync<BookViewModel>();

                await GetAuthorsAndGenres(response.ResponseBody.Genre.Id, response.ResponseBody.Author.Id);

                return View(response.ResponseBody);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Book/GetBookById/{id}")
                    .WithJsonContent()
                    .GetAsync<BookViewModel>();

                return View(response.ResponseBody);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(BookViewModel book)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, "api/Book/CreateBook")
                    .WithJsonContent()
                    .PostAsync(book);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedBook"></param>
        /// <param name="id"></param>
        /// <param name="genreEdit">todo: describe genreEdit parameter on Edit</param>
        /// <param name="authorEdit">todo: describe authorEdit parameter on Edit</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(BookViewModel updatedBook, int id, int genreEdit, int authorEdit)
        {

            updatedBook.AuthorId = authorEdit;
            updatedBook.GenreId = genreEdit;

            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Book/UpdateBook/{id}")
                    .WithJsonContent()
                    .PutAsync(updatedBook);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(BookViewModel book, int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Book/DeleteBook/{id}")
                    .WithJsonContent()
                    .PostAsync(book);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #endregion

        private async Task GetAuthorsAndGenres(int? selectedGenre, int? selectedAuthor)
        {
            var genres = await ApiCaller
            .FromServiceEndpoint(ServiceEndpoint, "api/Genre/GetAllGenres")
            .WithJsonContent()
            .GetAsync<IEnumerable<GenreViewModel>>();

            var authors = await ApiCaller
                .FromServiceEndpoint(ServiceEndpoint, "api/Author/GetAllAuthors")
                .WithJsonContent()
                .GetAsync<IEnumerable<AuthorViewModel>>();

            ViewBag.GenreId = new SelectList(genres.ResponseBody.ToList(), "Id", "Name");
            ViewBag.AuthorId = new SelectList(authors.ResponseBody.ToList(), "Id", "Name");

            ViewBag.GenreEdit = new SelectList(genres.ResponseBody.ToList(), "Id", "Name", selectedGenre);
            ViewBag.AuthorEdit = new SelectList(authors.ResponseBody.ToList(), "Id", "Name", selectedAuthor);
        }
    }
}