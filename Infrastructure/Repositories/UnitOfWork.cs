using Core;
using Core.Entities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<int> Compelete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();


        public IGenericRepositeory<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_context);
                
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepositeory<TEntity>)_repositories[type];
        }
    }
}
