using Movies.Core;
using Movies.Core.Models;
using Movies.Core.Repository;
using Movies.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;
        //public IGenericRepository<Genre> Genres { get; set; }

        //public IGenericRepository<Movie> Movies { get; set; }

        public UnitOfWork(ApplicationDbContext context /* ,IGenericRepository<Genre> genreRepository, IGenericRepository<Movie> movieRepository */)
        {
            _context = context;
            //Genres = genreRepository;
            //Movies = movieRepository;
        }

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;
        }
    }
}
