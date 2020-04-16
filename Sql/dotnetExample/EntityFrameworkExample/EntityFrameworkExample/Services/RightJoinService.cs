using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Services
{
    public class RightJoinService : BaseJoinService<Customer>
    {
        public RightJoinService(AppDbContext db, BaseTableWriter<Customer> tableWriter)
            : base(db, tableWriter)
        {

        }

        public override void Query()
        {
            var customerModels = new List<Customer>();

            var result = from customers in _db.Customers
                         join offer in _db.Offers on customers.Id equals offer.CustomerId
                         into Offer
                         from o in Offer.DefaultIfEmpty()
                         select new
                         {
                             Customer = customers,
                             Offer = o
                         };

            foreach (var row in result)
            {
                if (!customerModels.Any(m => m.Id == row.Customer.Id))
                {
                    customerModels.Add(row.Customer);
                }

                Customer customer = customerModels.Single(c => c.Id == row.Customer.Id);
                
                if (row.Offer != null)
                {
                    customer.Offers.Add(row.Offer);
                }
                else
                {
                    customer.Offers.Add(new Offer
                    {
                        Id = -1,
                        Name = "null"
                    });
                }
            }

            _tableWriter.Models = customerModels;
        }
    }
}
