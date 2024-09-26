using Movies.Core.Models;
using Movies.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity; 
        //public IGenericRepository<Genre> Genres { get; set; }
        //public IGenericRepository<Movie> Movies { get; set; }
        Task<int> CompleteAsync();
    }
}
