using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        // Kategorileri almak için metod
        List<Category> GetCategories();

        // Sadece kategori adlarını almak için
        List<string> GetCategoryNames();

        List<Category> GetAll();
        Category TGetByID(List<int> categoryIds);
    }
}
