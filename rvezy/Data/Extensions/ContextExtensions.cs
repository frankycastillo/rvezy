using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace rvezy.Data.Extensions
{
    public static class ContextExtensions
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, Microsoft.EntityFrameworkCore.DbContext ctx, IEnumerable<T> range)
            where T : class, IBaseModel
        {
            foreach (var item in range) AddOrUpdate(dbSet, ctx, item);
        }

        public static void AddOrUpdate<T>(this DbSet<T> dbSet, Microsoft.EntityFrameworkCore.DbContext ctx, T entity) where T : class, IBaseModel
        {
            var exists = dbSet.AsNoTracking().Any(x => x.Id == entity.Id);
            if (exists)
                dbSet.Update(entity);
            else
                dbSet.Add(entity);
        }

        public static void DeleteAll<T>(this Microsoft.EntityFrameworkCore.DbContext context) where T : class
        {
            var items = context.Set<T>().ToList();
            context.Set<T>().RemoveRange(items);
            context.SaveChanges();
        }

        public static string GetTableNameWithSchema<T>(this Microsoft.EntityFrameworkCore.DbContext context) where T : class
        {
            var entityType = typeof(T).FullName;
            if (entityType != null)
            {
                var mapping = context.Model.FindEntityType(entityType);
                return mapping.GetSchemaQualifiedTableName();
            }

            return null;
        }
    }
}