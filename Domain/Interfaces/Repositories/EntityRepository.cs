using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IEntityRepository<T> where T : class
    { 

        Task<T> GetByIdAsync(int id);
        
        Task<List<T>> GetAllAsync(int pageNumber, int pageSize);

        Task AddAsync(T entity);

        void Remove(T entity);

        Task<int> SaveChangesAsync();

        void Update(T entity);
    }

}
