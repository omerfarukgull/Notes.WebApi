using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<Category> GetById(int id);
        Task<Category> AddAsync(Category category);
    }
}
