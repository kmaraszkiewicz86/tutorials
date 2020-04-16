using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExample.Services
{
    public class InnerJoinService: BaseJoinService<Customer>
    {
        public InnerJoinService(AppDbContext db, BaseTableWriter<Customer> tableWriter) : base(db, tableWriter)
        {
        }

        public override void Query()
        {
            _tableWriter.Models = _db.Customers.Include(c => c.Offers);
        }
    }
}
