using System.Collections.Generic;
using System.Linq;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EntityFrameworkExample.Services
{
    public class InnerJoinService: BaseJoinService<Customer>
    {
        public InnerJoinService(AppDbContext db, BaseTableWriter<Customer> tableWriter) : base(db, tableWriter)
        {
        }

        public override void Query()
        {
            var result = _db.Customers.Join(
                _db.Offers,
                (Customer c) => c.Id,
                (Offer o) => o.CustomerId,
                (c, o) => new
                {
                    Customer = c,
                    Offer = o
                });

            var customers = new List<Customer>();

            foreach (var row in result)
            {
                if (customers.All(c => c.Id != row.Customer.Id))
                {
                    customers.Add(row.Customer);
                }

                var customer = customers.Single(c => c.Id == row.Customer.Id);

                if (row.Offer != null)
                {
                    customer.Offers.Add(row.Offer);
                }
                else
                {
                    customer.Offers.Add(new Offer
                    {
                        Id = 0,
                        Name = "null"
                    });
                }
            }

            _tableWriter.Models = customers;


        }
    }
}
