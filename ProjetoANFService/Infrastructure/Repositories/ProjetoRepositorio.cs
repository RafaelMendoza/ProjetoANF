using ProjetoANFService.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjetoANFService.Infrastructure.Repositories
{
    //TODO: Logica Update
    //UpdateAsync
    //Rever implementação do repositório.

    /// <summary>
    /// Repository that will manage entityes to/from database.
    /// </summary>
    public class ProjetoRepositorio<TContext> : IRepositoryOld where TContext : DbContext
    {
        #region Attributes

        private readonly TContext _dbContext;

        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        /// <summary>
        /// This constructor recieves an injected Entity Framework Context.
        /// The Injection is made by the SimpleInjector Framework.
        /// </summary>
        /// <param name="dbContext">DbContext to be used by the Repository.</param>
        public ProjetoRepositorio(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public

        #region Get

        /// <summary>
        /// Get all registers from the database for a given entity.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned.</typeparam>
        /// <returns>
        /// A collection of entities.
        /// </returns>
        public virtual ICollection<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>().ToList();
        }

        /// <summary>
        /// Get all registers from the database for a given entity asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned.</typeparam>
        /// <returns>
        /// A task that, when completed, contains a collection of entities.
        /// </returns>
        public virtual async Task<ICollection<T>> GetAllAsync<T>() where T : class
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets an entity by Id.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned.</typeparam>
        /// <param name="id">The Id of the entity to be searched.</param>
        /// <returns>
        /// An entity  searched by the provided Id.
        /// </returns>
        public virtual T GetById<T>(object id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Gets an entity by Id asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned.</typeparam>
        /// <param name="id">The Id of the entity to be searched.</param>
        /// <returns>
        /// An entity  searched by the provided Id.
        /// </returns>
        public virtual async Task<T> GetByIdAsync<T>(object id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets a collection of entities using a filter.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned</typeparam>
        /// <param name="predicate">The set of filters to be used.</param>
        /// <returns>
        /// A collection of entities that was retrieved by filtering.
        /// </returns>
        public virtual ICollection<T> GetByFilter<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// Gets a collection of entities using a filter.
        /// </summary>
        /// <typeparam name="T">Type of entity to be returned</typeparam>
        /// <param name="predicate">The set of filters to be used.</param>
        /// <returns>
        /// A collection of entities that was retrieved by filtering.
        /// </returns>
        public virtual async Task<ICollection<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        #endregion

        #region Create, Update and Delete

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Create<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Update<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region SaveChanges

        /// <summary>
        /// Save the changes made to the database.
        /// </summary>
        /// <returns>
        /// An int indicating if the changes were successfully made or not.
        /// </returns>
        public virtual int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Save the changes made to the database asynchronously.
        /// </summary>
        /// <returns>
        /// An int indicating if the changes were successfully made or not.
        /// </returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose(bool disposing)
        {
            if(disposing)
            {

            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}