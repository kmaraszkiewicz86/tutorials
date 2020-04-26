using System.Linq;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models.Views;

namespace EntityFrameworkExample.Services
{
    public class FullJoinService : BaseJoinService<CustomerOffer>
    {
        public FullJoinService(AppDbContext db, 
            BaseTableWriter<CustomerOffer> tableWriter)
            : base(db, tableWriter)
        {

        }

        public override void Query()
        {
            _tableWriter.Models = _db.CustomerOfferFullJoins.ToList();
        }
    }
}
