/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Application.Static;
using Core.Repositories.Base;
using Core.Repositories.UnitOfwork;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ClinicDBContext _context;

        public Repository(ClinicDBContext context)
        {
            _context = context;
        }

        // Searching
        public async Task<List<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            // Includes
            if (includes != null)
                SetQueryIncludes(query, includes);
            // Result
            return await query.ToListAsync();

        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null,string orderByType = OrderBy.Ascending)
        { // Expression
            IQueryable<T> query = _context.Set<T>();
            if (orderBy != null)
            {
                if (orderByType == OrderBy.Ascending)
                    query.OrderBy(orderBy);
                else
                    query.OrderByDescending(orderBy);
            }
            // Result
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            string[] includes = null)
        {
            // Expression
            IQueryable<T> query = _context.Set<T>().Where(expression);
            // Includes
            if (includes != null)

                SetQueryIncludes(query, includes);
            // Result
            return await query.ToListAsync();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            string[] includes = null,
            Expression<Func<T, object>> orderBy = null,
            string orderByType = OrderBy.Ascending)
        {
            // Expression
            IQueryable<T> query = _context.Set<T>().Where(expression);
            // Includes
            if (includes != null)
                SetQueryIncludes(query, includes);
            // Ordering
            if (orderBy != null)
            {
                if (orderByType == OrderBy.Ascending)
                    query.OrderBy(orderBy);
                else
                    query.OrderByDescending(orderBy);
            }
            // Result
            return await query.ToListAsync();

        }
        public async Task<List<T>> GetAllAsync(int take,
            int skip,
            Expression<Func<T, bool>> expression = null,
            string[] includes = null,
            Expression<Func<T, object>> orderBy = null,
            string orderByType = OrderBy.Ascending)
        {

            // Expression
            IQueryable<T> query = _context.Set<T>();
            if (expression != null)
                query.Where(expression);
            // Includes
            if (includes != null)
                SetQueryIncludes(query, includes);
            // Pagination
            query = query.Skip(skip);
            query = query.Take(take);
            // Ordering
            if (orderBy != null)
            {
                if (orderByType == OrderBy.Ascending)
                    query.OrderBy(orderBy);
                else
                    query.OrderByDescending(orderBy);
            }
            // Result
            return await query.ToListAsync();

        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            // Expression
            IQueryable<T> query = _context.Set<T>().Where(expression);
            // Includes
            if (includes != null)
                SetQueryIncludes(query, includes);
            // Result
            return await query.FirstOrDefaultAsync();
        }
        // Adding
        public async Task<bool> AddAsync(T entity)
        {
            if (entity == null) return false;
            await _context.Set<T>().AddAsync(entity);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null) return false;
            await _context.Set<T>().AddRangeAsync(entities);
            return true;
        }
        // Updating
        public bool Update(T entity)
        {
            if (entity == null) return false;
            _context.Set<T>().Update(entity);
            return true;
        }
        public bool Update(T oldEntity, T newEntity)
        {
            if (oldEntity == null || newEntity == null) return false;
            _context.Entry<T>(oldEntity).CurrentValues.SetValues(newEntity);
            return true;
        }

        // Deleting
        public bool Delete(T entity)
        {
            if (entity == null) return false;
            _context.Set<T>().Remove(entity);
            return true;
        }

        public bool DeleteRange(IEnumerable<T> entities)
        {
            if (entities == null) return false;
            _context.Set<T>().RemoveRange(entities);
            return true;
        }
        // Extra
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            // Expression
            IQueryable<T> query = _context.Set<T>();
            if (expression != null)
                query.Where(expression);
            // Result
            return await query.CountAsync();
        }
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> expression)
        {
            // Expression
            IQueryable<T> query = _context.Set<T>();
            // Result
            return await query.MaxAsync(expression);
        }
        // Config
        private void SetQueryIncludes(IQueryable<T> query, string[] includes)
        {
            foreach (var include in includes)
                query = query.Include(include);

        }

        
    }
}
