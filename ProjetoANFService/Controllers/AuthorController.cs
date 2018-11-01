using ProjetoANFService.Infrastructure.Contracts;
using ProjetoANFService.Models;
using ProjetoANFService.Models.Error;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjetoANFService.Controllers
{
    /// <summary>
    /// Handle CRUD operations for Author entities from/to the repository. Inherits from the ApiController class. 
    /// </summary>
    [RoutePrefix("api/Author")]
    public class AuthorController : ApiController
    {

        #region Attributes

        private readonly IRepository _repository;

        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        public AuthorController(IRepository repository)
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
        [Route("GetAllAuthors", Name = "GetAllAuthorsAsync")]
        public async Task<IHttpActionResult> GetAllAuthorsAsync()
        {

            try
            {
                var authors = await _repository.GetAllAsync<Author>();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return new ErrorMessageResult(Request, ex.Message,
                    ex.InnerException.Message, HttpStatusCode.BadRequest);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAuthorById/{id:int}", Name = "GetAuthorByIdAsync")]
        public async Task<IHttpActionResult> GetAuthorByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync<Author>(id);

            return Ok(author);
        }

        #endregion

        #region Post

        /// <summary>
        /// 
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateOneAuthor", Name = "CreateOneAuthorAsync")]
        public async Task<IHttpActionResult> CreateOneAuthorAsync([FromBody]Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Create(author);
                await _repository.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601:
                        return new ErrorMessageResult(Request, $"Já existe um Autor com o nome {author.Name} na base de dados.",
                            "Nome Duplicado", HttpStatusCode.BadRequest);
                    default:
                        throw new Exception(ex.Message);
                }
            }
            catch(Exception ex)
            {
                return new ErrorMessageResult(Request, ex.Message,
                    "Erro", HttpStatusCode.BadRequest);
            }

            return CreatedAtRoute("CreateOneAuthorAsync", new { author.Id }, author);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateAuthor", Name = "CreateAuthorAsync")]
        public async Task<IHttpActionResult> CreateAuthorAsync([FromBody]Author author)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Create(author);
                await _repository.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                switch(author.Id)
                {
                    case 2627:
                        return new ErrorMessageResult(Request, $"Já existe um Autor com o nome {author.Name} na base de dados.",
                            "Nome Duplicado", HttpStatusCode.BadRequest);
                    default:
                        throw new Exception(ex.Message);
                }
            }

            return CreatedAtRoute("CreateAuthor", new { author.Id }, author);
        }
        #endregion

        #region Put

        [HttpPut]
        [Route("UpdateAuthor/{id:int}", Name = "UpdateAuthorAsync")]
        public async Task<IHttpActionResult> UpdateAuthor([FromBody]Author updatedAuthor, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(updatedAuthor.Id != id)
            {
                return NotFound();
            }

            try
            {
                _repository.Update(updatedAuthor);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("DeleteAuthor/{id:int}", Name = "DeleteAuthorAsync")]
        public async Task<IHttpActionResult> DeleteAuthorAsync([FromBody]Author author, int id)
        {
            try
            {
                _repository.Delete(author);
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
