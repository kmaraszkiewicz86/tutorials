using EntityFrameworkExample.Core;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public class RightJoinServiceFactory: IJoinServiceFactory
    {
        public IJoinService CreateBaseJoinService(AppDbContext db)
        {
            return new RightJoinService(db, new CustomerOfferTableWriter());
        }
    }
}
