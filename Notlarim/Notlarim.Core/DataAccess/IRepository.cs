using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Core.DataAccess
{
    public interface IRepository<T> where T : class, new ()
    {
        // Expression<Func<T, bool>> fiter = null Bu kod bize Business katmanından isteğe göre kodu sağlar kullanmazı ve ekstara kod yazmamıza gerektirmez
        // Örneğin Business tan biz "Id" isteyen bir metot olsa normalde onun içinde ek olarak "T GetById(int id)" kod yazmamız gerekir
        // yada ürün listelerken örneğin Kategori, Yazar ,Renk için hepsi içi ayrı ayrı kod yazmamız gerekir

        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        void Create(T entity);
        void Update(T entity);
        Task CreateAsync(T entity);
     
        Task UpdateAsync(T entity);
        void Delete(T entity);
    }
}
