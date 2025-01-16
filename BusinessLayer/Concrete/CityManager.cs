// CityManager.cs
using BusinessLayer.Abstract;
using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CityManager : ICityService, IGenericService<City>  // IGenericService<City> eklendi
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        // Şehirleri al
        public List<City> GetCities()
        {
            return _cityDal.GetAllCities();
        }

        public List<string> GetCityNames()
        {
            // Kategorilerin sadece isimlerini al
            return _cityDal.GetList().Select(c => c.CityName).ToList();
        }

        // TAdd metodunu implement et
        public void TAdd(City city)
        {
            _cityDal.Insert(city);  // Veritabanına şehir ekle
        }

        // TDelete metodunu implement et
        public void TDelete(City city)
        {
            _cityDal.Delete(city);  // Şehri sil
        }

        // TUpdate metodunu implement et
        public void TUpdate(City city)
        {
            _cityDal.Update(city);  // Şehir bilgilerini güncelle
        }

        // TGetList metodunu implement et
        public List<City> TGetList()
        {
            return _cityDal.GetList();  // Şehirlerin listesini al
        }

        // TGetByID metodunu implement et
        public City TGetByID(int id)
        {
            return _cityDal.GetByID(id);  // Şehir id'ye göre bir şehir getir
        }
    }
}
