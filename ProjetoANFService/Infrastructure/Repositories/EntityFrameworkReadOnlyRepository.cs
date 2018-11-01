using ProjetoANFService.Infrastructure.Contracts;
using ProjetoANFService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Threading.Tasks;

namespace ProjetoANFService.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
        where TContext : DbContext
    {
        #region Attributes

        protected readonly TContext context;

        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_context"></param>
        public EntityFrameworkReadOnlyRepository(TContext _context)
        {
            context = _context;
        }

        #endregion

        #region Actions

        #region Get
        #endregion

        #region Post
        #endregion

        #region Put
        #endregion

        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll<T>(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(null, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync<T>(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : class, IEntity
        {
            return await GetQueryable<T>(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(filter, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : class, IEntity
        {
            return await GetQueryable<T>(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetOne<T>(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(filter, null, includeProperties).SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<T> GetOneAsync<T>(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null)
            where T : class, IEntity
        {
            return await GetQueryable<T>(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetFirst<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(filter, orderBy, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<T> GetFirstAsync<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
            where T : class, IEntity
        {
            return await GetQueryable<T>(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById<T>(object id)
            where T : class, IEntity
        {
            return context.Set<T>().Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync<T>(object id)
            where T : class, IEntity
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int GetCount<T>(Expression<Func<T, bool>> filter = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(filter).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class, IEntity
        {
            return await GetQueryable<T>(filter).CountAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool GetExists<T>(Expression<Func<T, bool>> filter = null)
            where T : class, IEntity
        {
            return GetQueryable<T>(filter).Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> GetExistsAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class, IEntity
        {
            return await  GetQueryable<T>(filter).AnyAsync();
        }
        #endregion

        #region Protected

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> GetQueryable<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where T : class, IEntity
        {
            includeProperties = includeProperties ?? String.Empty;
            IQueryable<T> query = context.Set<T>();

            if(filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var inclideProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(inclideProperty);
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if(skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if(take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        #endregion

        #endregion

        #region Events
        #endregion
    }
}