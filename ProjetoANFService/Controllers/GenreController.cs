using ProjetoANFService.Infrastructure.Contracts;
using ProjetoANFService.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjetoANFService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Genre")]
    public class GenreController : ApiController
    {
        #region Attributes

        private readonly IRepository _repository;

        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        public GenreController(IRepository repository)
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
        [Route("GetAllGenres", Name = "GetAllGenresAsync")]
        public async Task<IHttpActionResult> GetAllGenresAsync()
        {
            var genres = await _repository.GetAllAsync<Genre>();

            return Ok(genres);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGenreById/{id:int}", Name = "GetGenreByIdAsync")]
        public async Task<IHttpActionResult> GetGenreByIdAsync(int id)
        {
            var genre = await _repository.GetByIdAsync<Genre>(id);

            return Ok(genre);
        }

        #endregion

        #region Post

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateGenre", Name = "CreateGenreAsync")]
        public async Task<IHttpActionResult> CreateGenreAsync([FromBody]Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Create(genre);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return CreatedAtRoute("CreateGenre", new { genre.Id }, genre);
        }
        #endregion

        #region Put

        [HttpPut]
        [Route("UpdateGenre/{id:int}", Name = "UpdateGenreAsync")]
        public async Task<IHttpActionResult> UpdateGenre([FromBody]Genre updatedGenre, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedGenre.Id != id)
            {
                return NotFound();
            }

            try
            {
                _repository.Update(updatedGenre);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("DeleteGenre/{id:int}", Name = "DeleteGenreAsync")]
        public async Task<IHttpActionResult> DeleteGenreAsync([FromBody]Genre genre, int id)
        {
            try
            {
                _repository.Delete(genre);
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