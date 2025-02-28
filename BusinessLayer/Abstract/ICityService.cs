﻿using DataAccessLayer;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICityService : IGenericService<City>
    {
        List<City> GetCities();  // Şehirleri almak için metod
        List<string> GetCityNames();
    }
}
