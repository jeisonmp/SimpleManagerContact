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
                    Guid guidId = default(Guid);

                    if (!string.IsNullOrEmpty(fields.Sellers))
                    {
                        guidId = new Guid(fields.Sellers);
                        Guid.TryParse(fields.Sellers, out guidId);
                        if (!guidId.Equals(default(Guid)))
                        {
                            clients = clients.Where(o => o.User.UserId.Equals(guidId)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(fields.Classification))
                    {
                        guidId = new Guid(fields.Classification);
                        Guid.TryParse(fields.Classification, out guidId);
                        if (!guidId.Equals(default(Guid)))
                        {
                            clients = clients.Where(o => o.Classification.ClassificationId.Equals(guidId)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(fields.City))
                    {
                        guidId = new Guid(fields.City);
                        Guid.TryParse(fields.City, out guidId);
                        if (!guidId.Equals(default(Guid)))
                        {
                            clients = clients.Where(
                                o => 
                                    o.Region != null &&
                                    o.Region.City != null &&
                                    o.Region.CityId.Equals(guidId)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(fields.Region))
                    {
                        guidId = new Guid(fields.Region);
                        Guid.TryParse(fields.Region, out guidId);
                        if (!guidId.Equals(default(Guid)))
                        {
                            clients = clients.Where(o => o.Region != null && o.RegionId.Equals(guidId)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(fields.Gender))
                    {
                        clients = clients.Where(o => o.Gender.Equals(fields.Gender)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fields.Gender))
                    {
                        clients = clients.Where(o => o.Gender.Equals(fields.Gender)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fields.Name))
                    {
                        clients = clients.Where(o => o.Name.Contains(fields.Name)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fields.Name))
                    {
                        clients = clients.Where(o => o.Name.Contains(fields.Name)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fields.LastPurchase))
                    {
                        DateTime dt = DateTime.MinValue;
                        DateTime.TryParse(fields.LastPurchase, out dt);
                        if (!dt.Equals(DateTime.MinValue))
                        {
                            clients = clients.Where(o => o.LastPurchase >= dt).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(fields.Until))
                    {
                        DateTime dt = DateTime.MinValue;
                        DateTime.TryParse(fields.Until, out dt);
                        if (!dt.Equals(DateTime.MinValue))
                        {
                            clients = clients.Where(o => o.LastPurchase <= dt).ToList();
                        }
                    }
                }

                return clients;
            }
        }
    }
}