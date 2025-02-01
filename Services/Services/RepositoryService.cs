using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services
{       
    public class RepositoryService<T> : IRepositoryService<T> where T : class 
    {
        protected readonly AurAppointmentContext _context;
        private DbSet<T> entities;


        public RepositoryService(AurAppointmentContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> Get(Expression<Func<T, bool>> where)
        {
            try
            {

                return await entities.Where(where).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public T GetById(int id)
        {
            try
            {

                return _context.Set<T>().Find(id);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IEnumerable<T> GetByConsultant(int Consultant_Id)
        //{
        //    return entities.AsEnumerable().ToList().Where(s => s.Consultant_Id == Consultant_Id);
        //}
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
        }

        public void Delete(T entity)
        {
            //T entity = entities.SingleOrDefault(s => s.id == id);
            //entities.Remove(entity);

            _context.Set<T>().Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = entities.Where<T>(where).AsEnumerable();

            foreach (T obj in objects)
                entities.Remove(obj);
        }

        public void testEntity(T entity)
        {

        }
        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> where)
        {
            try
            {
                var entity = await entities.Where(where).ToListAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return entities;
            }
        }
        public async Task<PagedResult<T>> GetAllPagedList(int currentPage, int pageSize, string sortOn = null, string sortOrder = null)
        {
            return await entities.AddCustomWhere(this._context, null).GetPaged(currentPage, pageSize, sortOn, sortOrder);
        }

        public async Task<PagedResult<T>> GetExpressionAllPagedList(int currentPage, int pageSize, string sortOn = null, string sortOrder = null, Expression<Func<T, bool>> expression = null)
        {

            return await entities.AddCustomWhere(this._context, expression).GetPaged(currentPage, pageSize, sortOn, sortOrder);
        }

        public void Update(T entity, int id)
        {
            try
            {

                if (entity == null) throw new ArgumentNullException("entity");

                var entity1 = entities.Find(id);
                if (entity1 == null)
                {
                    return;
                }

                _context.Entry(entity1).CurrentValues.SetValues(entity);

                entities.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    
}
