using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.core.Controllers
{
    public partial class RegionController
    {
        public List<Region> GetList(Guid? CityId)
        {
            using (var db = new DBModel())
            {
                var regions = db.Region.OrderBy(o => o.RegionName).ToList();

                if (CityId != null)
                {
                    regions = regions.Where(o => o.CityId.Equals(CityId)).ToList();
                }

                return regions;
            }
        }
    }
}