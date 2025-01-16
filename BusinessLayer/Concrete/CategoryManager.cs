using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IGenericDal<Category> _categoryDal;

        public CategoryManager(IGenericDal<Category> categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public void TUpdate(Category category)
        {
            _categoryDal.Update(category); // Kategoriyi güncelleme
        }

        public void TDelete(Category category)
        {
            _categoryDal.Delete(category);
        }

        
        public Category TGetByID(List<int> categoryIds)
        {
            throw new NotImplementedException();
        }

        public Category TGetByID(int id)
        {
            using (var c = new Context())
            {
                return c.Categories.FirstOrDefault(c => c.CategoryId == id);
            }
        }

        public List<Category> TGetList()
        {
            return _categoryDal.GetList();  // Kategorilerin tüm listesini al
        }

        // GetCategories metodu TGetList metodu üzerinden kategori listesini döndürüyor
        public List<Category> GetCategories()
        {
            return TGetList();  // Burada zaten var olan TGetList metodunu kullanıyoruz
        }

        public List<string> GetCategoryNames()
        {
            // Kategorilerin sadece isimlerini al
            return _categoryDal.GetList().Select(c => c.Name).ToList();
        }

        public List<Category> GetAll()
        {
            using (var c = new Context())
            {
                return c.Categories.ToList();
            }
        }

    }
}
