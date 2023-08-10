using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Notlarim.Core.DataAccess.EntityFramework
{
    public class EfCoreGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {

        protected readonly DbContext context;
        public EfCoreGenericRepository(DbContext dbContext)
        {
            context = dbContext;
        }

        public void Create(TEntity entity)
        {
            //Entry() bize ulaşacağımız sınıf üzerinde(TEntity) nesneye erişim ve değiştirme hakkı atanır

            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();

        }
        public async Task CreateAsync(TEntity entity)
        {
             context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();

        }
        public void Delete(TEntity entity)
        {

            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();

        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            // _context.Set<TEntity> ise veri tabanı tablosu için bir DbSet nesnesi almak için kullanılan yöntemdir. <TEntity> yerine, o tabloya karşılık gelen nesne türünün adını belirtmelisiniz.
            //Bu, genellikle bir sınıf adıdır ve veri tabanındaki tablo ile eşleşir.

            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);

        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {

            return await (filter == null
        ? context.Set<TEntity>().ToListAsync()
        : context.Set<TEntity>().Where(filter).ToListAsync());

        }

        public void Update(TEntity entity)
        {

            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
