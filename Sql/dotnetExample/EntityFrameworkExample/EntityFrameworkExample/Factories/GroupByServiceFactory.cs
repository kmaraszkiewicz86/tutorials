using EntityFrameworkExample.Core;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public class GroupByServiceFactory: IJoinServiceFactory
    {
        public IJoinService CreateBaseJoinService(AppDbContext db)
        {
            return new GroupByService(db, new CustomerWithTotalPriceModelWriter());
        }
    }
}
