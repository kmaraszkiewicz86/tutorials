using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EntityFrameworkExample.Services
{
    public class GroupByService: BaseJoinService<CustomerWithTotalPriceModel>
    {
        public GroupByService(AppDbContext db, BaseTableWriter<CustomerWithTotalPriceModel> tableWriter) : base(db, tableWriter)
        {
        }

        public override void Query()
        {
            _tableWriter.Models = from customer in _db.Customers
                join offer in _db.Offers on customer.Id equals offer.CustomerId
                group offer by customer.Name
                into grouping
                select new CustomerWithTotalPriceModel
                {
                    Name = grouping.Key,
                    TotalPrice = grouping.Sum(o => o.Price)
                }
                into customerWithTotalPriceModel
                orderby customerWithTotalPriceModel.TotalPrice descending 
                select customerWithTotalPriceModel;
        }
    }
}
