using EntityFrameworkExample.Core;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public class FullJoinServiceFactory: IJoinServiceFactory
    {
        public IJoinService CreateBaseJoinService(AppDbContext db)
        {
            return new FullJoinService(db, new CustomerOfferTableWriter());
        }
    }
}
