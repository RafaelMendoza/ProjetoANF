using ProjetoANFCore.Contracts;
using ProjetoANFCore.Entities;
using ProjetoANFCore.Helpers;
using ProjetoANFWeb.Controllers.Base;
using ProjetoANFWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjetoANFWeb.Controllers
{
    /// <summary>
    /// Handle Author CRUD operations from the views/user input to the server. Inherits from the BaseController.
    /// </summary>
    /// <remarks>
    /// The variable ServiceEndpoint is from the BaseController, and so it's needed for the correct behaviour of
    /// this class.
    /// The ServiceEndpoint is the Web API URL
    /// </remarks>
    public class AuthorController : BaseController
    {
        #region Attributes
        
        /// <summary>
        /// Contains the response from the Web Api for a single Author entity.
        /// </summary>
        IApiResponse<AuthorViewModel> _singleAuthorResponse;
        /// <summary>
        /// Contains the response from the Web Api for an Author entity collection.
        /// </summary>
        IApiResponse<IEnumerable<AuthorViewModel>> _authorListResponse;

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
        /// Gets a collection of all Authors registered on the server and returns the Index view.
        /// </summary>
        /// <returns>
        /// A task that, when completed, returns the Index view with a collection of Authors as model.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                _authorListResponse = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, "api/Author/GetAllAuthors")
                    .WithJsonContent()
                    .GetAsync<IEnumerable<AuthorViewModel>>();

                if (!_authorListResponse.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel
                    {
                        Title = _authorListResponse.ErrorMessage,
                        Message = _authorListResponse.ReasonPhrase
                    });
                }

                return View(_authorListResponse.ResponseBody.ToList() ?? new List<AuthorViewModel>());
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Title = ex.Message, Message = ex.ToString() });
            }

            
        }

        /// <summary>
        /// Returns the Create view. This view is a form to create new Authors.
        /// </summary>
        /// <returns>
        /// The Create view.
        /// </returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Gets a single Author from the database and returns on the Edit view.
        /// </summary>
        /// <param name="id">AuthorViewModel Id.</param>
        /// <returns>
        /// A task that, when completed, contains the Edit view using a AuthorViewModel retrieved from the server
        /// as model.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                _singleAuthorResponse = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Author/GetAuthorById/{id}")
                    .WithJsonContent()
                    .GetAsync<AuthorViewModel>();

            }
            catch (Exception ex)
            {
                throw new ViewException(ex.Message);
            }

            if(_singleAuthorResponse.IsSuccessStatusCode)
            {
                return View(_singleAuthorResponse.ResponseBody);
            }

            throw new ViewException(_singleAuthorResponse.StatusCode,
                _singleAuthorResponse.ErrorMessage, _singleAuthorResponse.ReasonPhrase);
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
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Author/GetAuthorById/{id}")
                    .WithJsonContent()
                    .GetAsync<AuthorViewModel>();

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
        /// Posts an Author to the server.
        /// </summary>
        /// <param name="author"></param>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> Create(AuthorViewModel author)
        {
            try
            {
                _singleAuthorResponse = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, "api/Author/CreateOneAuthor")
                    .WithJsonContent()
                    .PostAsync(author);
            }
            catch (Exception ex)
            {
                throw new ViewException(ex.Message);
            }

            if (_singleAuthorResponse.IsSuccessStatusCode)
            {
                return Json(new { url = Url.Action("Index") });
            }

            throw new ViewException(_singleAuthorResponse.StatusCode,
                _singleAuthorResponse.ErrorMessage, _singleAuthorResponse.ReasonPhrase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedAuthor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(AuthorViewModel updatedAuthor, int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Author/UpdateAuthor/{id}")
                    .WithJsonContent()
                    .PutAsync(updatedAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (_singleAuthorResponse.IsSuccessStatusCode)
            {
                return View(_singleAuthorResponse.ResponseBody);
            }

            throw new Exception();
            //throw new ViewException(_singleAuthorResponse.Request,
            //    _singleAuthorResponse.ErrorMessage, _singleAuthorResponse.ReasonPhrase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="author"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(AuthorViewModel author, int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Author/DeleteAuthor/{id}")
                    .WithJsonContent()
                    .PostAsync(author);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Put
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