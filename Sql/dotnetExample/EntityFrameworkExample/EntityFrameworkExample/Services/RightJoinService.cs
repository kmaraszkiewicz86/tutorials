using System.Linq;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models.Views;

namespace EntityFrameworkExample.Services
{
    public class RightJoinService : BaseJoinService<CustomerOffer>
    {
        public RightJoinService(AppDbContext db, 
            BaseTableWriter<CustomerOffer> tableWriter)
            : base(db, tableWriter)
        {

        }

        public override void Query()
        {
            _tableWriter.Models = _db.CustomerOfferRightJoins.ToList();
        }
    }
}
