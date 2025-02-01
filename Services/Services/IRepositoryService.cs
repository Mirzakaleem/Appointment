using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IRepositoryService<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> Get(Expression<Func<T,bool>> where);

        T GetById(int Id);

        void Insert(T entity);
        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        void Update(T entity,int id);

        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where);

        Task<PagedResult<T>> GetAllPagedList(int currentPage, int pageSize, string sortOn = null, string sortOrder = null);

    }
}
