using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.core.Controllers
{
    public partial class CityController
    {
        public List<City> GetList()
        {
            using (var db = new DBModel())
            {
                return db.City.OrderBy(o => o.CityName).ToList();
            }
        }
    }
}