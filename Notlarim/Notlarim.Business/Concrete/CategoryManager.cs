using Notlarim.Business.Abstract;
using Notlarim.Data.Abstract;
using Notlarim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(Category category)
        {
            _categoryRepository.Create(category);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _categoryRepository.CreateAsync(category);
            return category;
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryRepository.GetList();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.Get(c => c.CategoryId == id);
        }


        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
