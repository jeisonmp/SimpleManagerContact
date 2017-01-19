using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleManagerContact.model;

namespace SimpleManagerContact.core.Controllers
{
    public partial class ClientController
    {
        public List<Client> GetList(User user)
        {
            using (var db = new DBModel())
            {
                var clients = db.Client
                    .Include("Region")
                    .Include("Region.City")
                    .Include("Classification")
                    .Include("User")
                    .ToList();

                if (!user.Name.ToUpper().Contains("ADMIN"))
                {
                    clients = clients.Where(o => o.SellerId.Equals(user.UserId)).ToList();
                }

                return clients;
            }
        }

        public List<Client> Search(dynamic fields, User user)
        {
            using (var db = new DBModel())
            {
                var clients = GetList(user);

                if (clients.Count > 0)
                {
                    Guid SellerId = new Guid(fields.Sellers);
                    Guid.TryParse(fields.Sellers, out SellerId);
                    if (!SellerId.Equals(default(Guid)))
                    {
                        clients = clients.Where(o => o.User.UserId.Equals(SellerId)).ToList();
                    }
                }

                return clients;
            }
        }
    }
}