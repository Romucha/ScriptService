using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ScriptService.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ScriptService.DataManagement.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public Task<T> GetById(int id, Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public Task Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
