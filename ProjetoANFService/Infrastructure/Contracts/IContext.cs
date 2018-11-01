using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ProjetoANFService.Infrastructure.Contracts
{
    public interface IContext: IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}