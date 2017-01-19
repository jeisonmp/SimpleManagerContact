using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.core.Controllers
{
    public class UserController
    {
        public User Logged(string username, string password)
        {
            using (var db = new DBModel())
            {
                return db.User.Where(o => o.Email == username && o.Password == password).FirstOrDefault();
            }
        }

        public List<User> GetList()
        {
            using (var db = new DBModel())
            {
                return db.User.OrderBy(o => o.Name).ToList();
            }
        }
    }
}