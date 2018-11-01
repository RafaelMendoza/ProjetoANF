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
    /// 
    /// </summary>
    public class GenreController : BaseController
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
                    .FromServiceEndpoint(ServiceEndpoint, "api/Genre/GetAllGenres")
                    .WithJsonContent()
                    .GetAsync<IEnumerable<GenreViewModel>>();

                return View(response.ResponseBody.ToList() ?? new List<GenreViewModel>());
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
        public ActionResult Create()
        {
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
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Genre/GetGenreById/{id}")
                    .WithJsonContent()
                    .GetAsync<GenreViewModel>();

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
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Genre/GetGenreById/{id}")
                    .WithJsonContent()
                    .GetAsync<GenreViewModel>();

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
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(GenreViewModel genre)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, "api/Genre/CreateGenre")
                    .WithJsonContent()
                    .PostAsync(genre);

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
        /// <param name="updatedGenre"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(GenreViewModel updatedGenre, int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Genre/UpdateGenre/{id}")
                    .WithJsonContent()
                    .PutAsync(updatedGenre);

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
        /// <param name="genre"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(GenreViewModel genre, int id)
        {
            try
            {
                var response = await ApiCaller
                    .FromServiceEndpoint(ServiceEndpoint, $"api/Genre/DeleteGenre/{id}")
                    .WithJsonContent()
                    .PostAsync(genre);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #endregion
    }
}