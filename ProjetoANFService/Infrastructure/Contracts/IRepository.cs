using ProjetoANFService.Models.Contracts;
using System.Threading.Tasks;

namespace ProjetoANFService.Infrastructure.Contracts
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<T>(T entity, string createdBy = null)
            where T : class, IEntity;

        void Update<T>(T entity, string modifiedBy = null)
            where T : class, IEntity;

        void Delete<T>(T entity)
            where T : class, IEntity;

        void Delete<T>(object id)
            where T : class, IEntity;

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
