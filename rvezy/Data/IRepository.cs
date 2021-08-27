using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using rvezy.Models;

namespace rvezy.Data
{
    public interface IRepository<T> : IDisposable where T : IBaseModel
    {
        #region Database

        Task<IDbContextTransaction> BeginTransaction();

        Task CommitTransaction(IDbContextTransaction transaction = null);

        Task RollbackTransaction(IDbContextTransaction transaction = null);

        Task<T> Add(T entity);

        Task<int> AddRange(IEnumerable<T> items);

        Task<int> Count();

        Task<int> Count(Expression<Func<T, bool>> match);

        Task<int> SoftDelete(T entity);

        Task<int> Delete(T entity);

        Task<ICollection<T>> Find(Expression<Func<T, bool>> predicate, IPaginator paginator = null, bool useCache = false);

        Task<ICollection<T>> FindAscending<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, IPaginator paginator = null, bool useCache = false);

        Task<ICollection<T>> FindDescending<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, IPaginator paginator = null, bool useCache = false);

        Task<ICollection<T>> FindFromSql(string query);

        Task<IEnumerable<T>> Distinct<TKey>(Func<T, TKey> keySelector);

        Task<ICollection<T>> GetAll(bool useCache = false);

        Task<ICollection<T>> GetAll(IPaginator paginator, bool useCache = false);

        Task<ICollection<T>> GetAllWithDeleted(IPaginator paginator, bool useCache = false);

        Task<ICollection<T>> GetAllAscending<TKey>(Expression<Func<T, TKey>> orderBy, IPaginator paginator, bool useCache = false);

        Task<ICollection<T>> GetAllDescending<TKey>(Expression<Func<T, TKey>> orderBy, IPaginator paginator, bool useCache = false);

        Task<T> Get(Guid id, bool useCache = false);

        Task<T> Get(Expression<Func<T, bool>> match, bool useCache = false);

        Task<T> GetFromSql(string query, bool useCache = false);

        Task<int> Save();

        Task<T> Update(T entity, object values, bool useCache = false);

        Task<T> Update(Guid id, object values, bool useCache = false);

        Task DeleteAll();

        string GetTableName();

        Task Truncate();

        #endregion
    }
}
