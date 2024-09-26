using Microsoft.EntityFrameworkCore;
using Movies.Core.Models;
using Movies.Core.Repository;
using Movies.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void UpdateAsync(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public void DeleteAsync(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);
    }
}
