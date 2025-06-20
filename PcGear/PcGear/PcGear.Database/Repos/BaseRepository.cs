using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcGear.Database.Repos
{
    public class BaseRepository<T>(PcGearDatabaseContext databaseContext) where T : BaseEntity
    {
        private DbSet<T> DbSet { get; } = databaseContext.Set<T>();



        public Task<List<T>> GetAllAsync(bool includeDeletedEntities = false)
        {
            return GetRecords(includeDeletedEntities).ToListAsync();
        }


        public Task<T?> GetFirstOrDefaultAsync(int primaryKey, bool includeDeletedEntities = false)
        {
            var records = GetRecords(includeDeletedEntities);
            return records.FirstOrDefaultAsync(record => record.Id == primaryKey);
        }


        public void Insert(params T[] records)
        {
            DbSet.AddRange(records);
        }

        public void Update(params T[] records)
        {
           foreach (var baseEntity in records)
            {
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
            DbSet.UpdateRange(records);
        }

        public void SoftDelete(params T[] records)
        {
            foreach (var baseEntity in records)
            {
                baseEntity.DeletedAt = DateTime.UtcNow;
            }
            Update(records);
        }

        public Task SaveChangesAsync()
        {
            return databaseContext.SaveChangesAsync();
        }

        protected IQueryable<T> GetRecords(bool includeDeletedEntities = false)
        {
            var result = DbSet.AsQueryable();

            if(includeDeletedEntities is false)
            {
                result=result.Where(record=>record.DeletedAt == null);
            }

            return result;
        }
    }
}
