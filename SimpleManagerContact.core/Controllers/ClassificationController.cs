using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.core.Controllers
{
    public partial class ClassificationController
    {
        public List<Classification> GetList()
        {
            using (var db = new DBModel())
            {
                return db.Classification.OrderBy(o => o.ClassificationName).ToList();
            }
        }
    }
}