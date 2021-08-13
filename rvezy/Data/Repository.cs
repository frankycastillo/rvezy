//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//
//namespace rvezy.Data
//{
//    public class Repository<T> : IRepository<T> where T : class, IBaseModel
//    {
//        private readonly TimeSpan _slidingCacheExpiry = TimeSpan.FromMinutes(30);
//        private readonly TimeSpan _absoluteCacheExpiry = TimeSpan.FromHours(8);
//
//        private readonly DbContext _context;
//        private readonly IHttpContextAccessor _contextAccessor;
//        private readonly SemaphoreSlim _dbContextLock;
//
//        protected readonly IDistributedCache Cache;
//        protected readonly ILogger Logger;
//
//        private bool _disposed;
//
//        protected Repository(DbContext context, IHttpContextAccessor contextAccessor, ILogger logger,
//            IDistributedCache cache)
//        {
//            Logger = logger;
//            Cache = cache;
//
//            _context = context;
//            _contextAccessor = contextAccessor;
//            _dbContextLock = new SemaphoreSlim(1, 1);
//        }
//
//        #region Helpers
//
//        public string GetTableName()
//        {
//            try
//            {
//                return _context.GetTableNameWithSchema<T>();
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetTableName: {e.Message} {e.StackTrace}");
//                throw;
//            }
//        }
//
//        #endregion
//
//        #region IDisposible
//
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//
//        protected virtual void Dispose(bool disposing)
//        {
//            if (!_disposed)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }
//
//                _disposed = true;
//            }
//        }
//
//        #endregion
//
//        #region Transactions
//
//        public async Task<IDbContextTransaction> BeginTransaction()
//        {
//            await _dbContextLock.WaitAsync().ConfigureAwait(false);
//            try
//            {
//                return await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task CommitTransaction(IDbContextTransaction transaction = null)
//        {
//            await _dbContextLock.WaitAsync().ConfigureAwait(false);
//            try
//            {
//                if (transaction != null)
//                {
//                    await Task.Run(() => transaction.Commit()).ConfigureAwait(false);
//                }
//                else
//                {
//                    await Task.Run(() => _context.Database.CommitTransaction()).ConfigureAwait(false);
//                }
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task RollbackTransaction(IDbContextTransaction transaction = null)
//        {
//            await _dbContextLock.WaitAsync().ConfigureAwait(false);
//            try
//            {
//                if (transaction != null)
//                {
//                    await Task.Run(() => transaction.Rollback()).ConfigureAwait(false);
//                }
//                else
//                {
//                    await Task.Run(() => _context.Database.RollbackTransaction()).ConfigureAwait(false);
//                }
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        #endregion
//
//        #region Methods
//
//        public virtual async Task<ICollection<T>> GetAll(bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_GetAll";
//
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().ToListAsync().ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetAll: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> GetAllAscending<TKey>(Expression<Func<T, TKey>> orderBy, IPaginator paginator,
//            bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_GetAllAscending";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().OrderBy(orderBy).Page(paginator).AsQueryable().ToListAsync()
//                    .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetAllAcending: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> GetAllDescending<TKey>(Expression<Func<T, TKey>> orderBy,
//            IPaginator paginator, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_GetAllDescending";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().OrderByDescending(orderBy).Page(paginator).AsQueryable()
//                    .ToListAsync()
//                    .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetAllDescending: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> GetAll(IPaginator paginator, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_GetAllUndeleted";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().Where(x => !x.Eliminado).OrderBy(x => x.Id).Page(paginator)
//                    .ToListAsync()
//                    .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry).ConfigureAwait(false);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetAll: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> GetAllWithDeleted(IPaginator paginator, bool useCache = false)
//        {
//            try
//            {
//                var cacheKey = $"{typeof(T).FullName}_GetAllWithDeleted";
//
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                var items = await _context.Set<T>().OrderBy(x => x.Id).Page(paginator).ToListAsync()
//                    .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetAll: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<T> Get(int? id, bool useCache = false)
//        {
//            if (!id.HasValue)
//            {
//                return null;
//            }
//
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_Get_{id}";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<T>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var item = await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, item, _slidingCacheExpiry);
//                }
//
//                return item;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Get: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<int> AddRange(IEnumerable<T> items)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                await _context.Set<T>().AddRangeAsync(items).ConfigureAwait(false);
//                await _context.SaveChangesAsync().ConfigureAwait(false);
//                return items.Count();
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.AddRange: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public virtual async Task<T> Add(T entity)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                _context.Set<T>().Add(entity);
//                await _context.SaveChangesAsync().ConfigureAwait(false);
//            }
//            catch (DbUpdateException dbe)
//            {
//                // var inner = dbe.InnerException as Sql;
//                // if (inner?.SqlState == "23505" && inner.Detail.Contains("hash", StringComparison.InvariantCultureIgnoreCase)) // Unique constraint violation on hash
//                // {
//                //     Logger.Debug($"Repository.Add: Duplicate hash error handled by HashManager");
//                //     throw dbe;
//                // }
//            }
//            catch (Exception e)
//            {
//                Logger.Error(
//                    $"Repository.Add: {e.Message} error inserting record: {entity.ToJson()} with stack: {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//
//            return entity;
//        }
//
//        public virtual async Task<T> Get(Expression<Func<T, bool>> match, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_Get_{match}";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<T>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().Where(match).ToListAsync().ConfigureAwait(false);
//                if (items.Count() > 1)
//                {
//                    Logger.Debug(
//                        $"Repository.Get: Getting by {match} was expecting exactly one, returned first match");
//                }
//
//                var item = items.FirstOrDefault();
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, item, _slidingCacheExpiry);
//                }
//
//                return item;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Get: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<T> GetFromSql(string query, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_Get_{query}";
//
//                if (useCache)
//                {
//                    var cached = await GetFromCache<T>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = await _context.Set<T>().FromSqlRaw(query).ToListAsync().ConfigureAwait(false);
//                if (items.Count() > 1)
//                {
//                    Logger.Debug($"Repository.GetFromSql: Getting by {query} was expecting exactly one, returned first match");
//                }
//
//                var item = items.FirstOrDefault();
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, item, _slidingCacheExpiry);
//                }
//
//                return item;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.GetFromSql: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> Find(Expression<Func<T, bool>> predicate, IPaginator paginator = null,
//            bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_Find_{predicate}";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = paginator != null
//                    ? await _context.Set<T>().Where(predicate).Page(paginator).ToListAsync().ConfigureAwait(false)
//                    : await _context.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Find: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> FindAscending<TKey>(Expression<Func<T, bool>> predicate,
//            Expression<Func<T, TKey>> orderBy, IPaginator paginator = null, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_FindAscending_{predicate}";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = paginator != null
//                    ? await _context.Set<T>().Where(predicate).OrderBy(orderBy).Page(paginator).AsQueryable()
//                        .ToListAsync().ConfigureAwait(false)
//                    : await _context.Set<T>().Where(predicate).OrderBy(orderBy).AsQueryable().ToListAsync()
//                        .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Find: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> FindDescending<TKey>(Expression<Func<T, bool>> predicate,
//            Expression<Func<T, TKey>> orderBy, IPaginator paginator = null, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                var cacheKey = $"{typeof(T).FullName}_FindDescending_{predicate}";
//                if (useCache)
//                {
//                    var cached = await GetFromCache<ICollection<T>>(cacheKey).ConfigureAwait(false);
//                    if (cached != null)
//                    {
//                        return cached;
//                    }
//                }
//
//                var items = paginator != null
//                    ? await _context.Set<T>().Where(predicate).OrderByDescending(orderBy).Page(paginator).AsQueryable()
//                        .ToListAsync().ConfigureAwait(false)
//                    : await _context.Set<T>().Where(predicate).OrderByDescending(orderBy).AsQueryable().ToListAsync()
//                        .ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, items, _slidingCacheExpiry);
//                }
//
//                return items;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Find: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<ICollection<T>> FindFromSql(string query)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                return await _context.Set<T>().FromSqlRaw(query).ToListAsync().ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.FindFromSql: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public virtual async Task<int> SoftDelete(T entity)
//        {
//            try
//            {
//                Guid currentUserId = GetCurrentUserId();
//                var values = new { Eliminado = true, EliminadoPor = currentUserId };
//                await Update(entity, values).ConfigureAwait(false);
//                return _context.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.SoftDelete: {e.Message} {e.StackTrace}");
//                throw;
//            }
//        }
//
//        public virtual async Task<int> Delete(T entity)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//
//                _context.Set<T>().Remove(entity);
//                return _context.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Delete: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<T> Update(int id, object values, bool useCache = false)
//        {
//            var cacheKey = $"{typeof(T).FullName}_Get_{id}";
//            if (useCache)
//            {
//                await DeleteFromCache(cacheKey).ConfigureAwait(false);
//            }
//
//            var current = await Get(id).ConfigureAwait(false);
//            if (current != null)
//            {
//                var item = await Update(current, values).ConfigureAwait(false);
//
//                if (useCache)
//                {
//                    await SetInCache(cacheKey, item, _slidingCacheExpiry);
//                }
//
//                return item;
//            }
//
//            return null;
//        }
//
//        public virtual async Task<T> Update(T entity, object values, bool useCache = false)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                if (entity == null)
//                {
//                    return null;
//                }
//
//                var exist = await _context.Set<T>().FindAsync(entity.Id).ConfigureAwait(false);
//                if (exist != null)
//                {
//                    try
//                    {
//                        _context.Entry(exist).CurrentValues.SetValues(values);
//                        await _context.SaveChangesAsync().ConfigureAwait(false);
//                    }
//                    catch (Exception ex)
//                    {
//                        Logger.Warn(
//                            $"Repository.Update: An error trying to update the record with ID: {entity.Id} ocurred, return existing. Error message: {ex.Message} {ex.StackTrace}");
//                        return exist;
//                    }
//                }
//
//                return exist;
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Update: An error occurred with message: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<int> Count(Expression<Func<T, bool>> match)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                return await _context.Set<T>().CountAsync(match).ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Count: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<int> Count()
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                return await _context.Set<T>().CountAsync().ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Count: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task<IEnumerable<T>> Distinct<TKey>(Func<T, TKey> keySelector)
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                var items = await _context.Set<T>().ToListAsync().ConfigureAwait(false);
//                return items.DistinctBy(keySelector);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Distinct: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public virtual async Task<int> Save()
//        {
//            try
//            {
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                return await _context.SaveChangesAsync().ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.Save: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task DeleteAll()
//        {
//            try
//            {
//                var items = await GetAll().ConfigureAwait(false);
//
//                await _dbContextLock.WaitAsync().ConfigureAwait(false);
//                _context.Set<T>().RemoveRange(items);
//                await _context.SaveChangesAsync().ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                Logger.Error($"Repository.DeleteAll: {e.Message} {e.StackTrace}");
//                throw;
//            }
//            finally
//            {
//                _dbContextLock.Release();
//            }
//        }
//
//        public async Task Truncate()
//        {
//            throw new NotImplementedException();
//
//            //             try
//            //             {
//            //                 await _dbContextLock.WaitAsync().ConfigureAwait(false);
//            //                 var tableName = _context.GetTableNameWithSchema<T>();
//            //                 if (tableName.IsNullOrWhiteSpace())
//            //                 {
//            //                     var modelName = typeof(T).FullName;
//            //                     throw new InvalidDataException($"Unable to resolve table name for {modelName}");
//            //                 }
//            //
//            //                 var command = $"TRUNCATE table {tableName}";
//            //
//            // #pragma warning disable EF1000 // Possible SQL injection vulnerability.
//            //                 await _context.Database.ExecuteSqlCommandAsync(command).ConfigureAwait(false);
//            // #pragma warning restore EF1000 // Possible SQL injection vulnerability.
//            //             }
//            //             catch (Exception e)
//            //             {
//            //                 Logger.Error($"Repository.Truncate: {e.Message} {e.StackTrace}");
//            //                 throw;
//            //             }
//            //             finally
//            //             {
//            //                 _dbContextLock.Release();
//            //             }
//        }
//
//        private Guid GetCurrentUserId()
//        {
//            var id = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (!id.IsNullOrWhiteSpace())
//            {
//                if (Guid.TryParse(id, out Guid userId))
//                {
//                    return userId;
//                }
//            }
//
//            return Guid.Empty;
//        }
//
//        private async Task<T1> GetFromCache<T1>(string cacheKey)
//        {
//            var cached = await Cache.GetStringAsync(cacheKey).ConfigureAwait(false);
//            if (cached == null) return default;
//            var result = JsonConvert.DeserializeObject<T1>(cached);
//            return result;
//        }
//
//        private async Task SetInCache<T1>(string cacheKey, T1 value, TimeSpan duration)
//        {
//            var encoded = JsonConvert.SerializeObject(value);
//            var options = new DistributedCacheEntryOptions()
//                .SetSlidingExpiration(duration)
//                .SetAbsoluteExpiration(DateTime.UtcNow.Add(_absoluteCacheExpiry));
//
//            await Cache.SetStringAsync(cacheKey, encoded, options).ConfigureAwait(false);
//        }
//
//        private async Task DeleteFromCache(string cacheKey)
//        {
//            await Cache.RemoveAsync(cacheKey).ConfigureAwait(false);
//        }
//
//        #endregion
//    }
//}
