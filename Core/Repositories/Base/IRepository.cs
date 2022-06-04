/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Application.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        // Searching
        Task<List<T>> GetAllAsync(string[] includes = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null, string orderByType = OrderBy.Ascending);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, string[] includes = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            string[] includes = null,
            Expression<Func<T, object>> orderBy = null,
            string orderByType = OrderBy.Ascending);
        Task<List<T>> GetAllAsync(int take,
            int skip,
            Expression<Func<T, bool>> expression = null,
            string[] includes = null,
            Expression<Func<T, object>> orderBy = null,
            string orderByType = OrderBy.Ascending);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, string[] includes = null);

        // Adding
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);

        // Updating
        bool Update(T oldEntity, T newEntity);
        bool Update(T entity);

        // Deleting
        bool Delete(T entity);
        bool DeleteRange(IEnumerable<T> entities);

        // Extra
        Task<int> CountAsync(Expression<Func<T, bool>> expression = null);
        Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> expression = null);
    }
}
