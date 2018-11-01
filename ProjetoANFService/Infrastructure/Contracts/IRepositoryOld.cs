using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjetoANFService.Infrastructure.Contracts
{
    public interface IRepositoryOld
    {
        ICollection<T> GetAll<T>() where T : class;
        Task<ICollection<T>> GetAllAsync<T>() where T : class;
        T GetById<T>(object id) where T : class;
        Task<T> GetByIdAsync<T>(object id) where T : class;
        ICollection<T> GetByFilter<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<ICollection<T>> GetByFilterAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Create<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
