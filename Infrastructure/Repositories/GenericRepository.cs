using Core;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepositeory<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }


        public async Task Add(T entity)
            => await _context.Set<T>().AddAsync(entity);
        

        public void Delete(T entity)
            =>  _context.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int? id)
           => await _context.Set<T>().FindAsync(id);
        public void Update(T entity)
            => _context.Set<T>().Update(entity);


        public async Task<T> GetEntityWithSpesificationsAsync(ISpecifications<T> specs)
            => await ApplySpesifications(specs).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpesificationsAsync(ISpecifications<T> specs)
            => await ApplySpesifications(specs).ToListAsync();


        private IQueryable<T> ApplySpesifications(ISpecifications<T> specs)
            => SpecificationEvaluater<T>.GetQuery(_context.Set<T>().AsQueryable(), specs);

        public async Task<int> CountAsync(ISpecifications<T> specifications)
            => await ApplySpesifications(specifications).CountAsync();
    }
}
