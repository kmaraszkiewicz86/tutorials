using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Services
{
    public class LeftJoinService : BaseJoinService<Customer>
    {
        public LeftJoinService(AppDbContext db, BaseTableWriter<Customer> tableWriter)
            : base(db, tableWriter)
        {

        }

        public override void Query()
        {
            var customerModels = new List<Customer>();

            var result = from customer in _db.Customers
                         join offer in _db.Offers on customer.Id equals offer.CustomerId
                         into offerJoin
                         from offer in offerJoin.DefaultIfEmpty()
                         select new
                         {
                             Customer = customer,
                             Offer = offer
                         };

            foreach (var row in result)
            {
                if (!customerModels.Any(m => m.Id == row.Customer.Id))
                {
                    customerModels.Add(row.Customer);
                }

                Customer customer = customerModels.Single(c => c.Id == row.Customer.Id);

                if (customer.Offers == null)
                {
                    customer.Offers = new HashSet<Offer>();
                }

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
