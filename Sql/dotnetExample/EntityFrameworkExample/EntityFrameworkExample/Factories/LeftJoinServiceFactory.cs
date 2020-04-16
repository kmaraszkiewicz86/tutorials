using EntityFrameworkExample.Core;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public class LeftJoinServiceFactory: IJoinServiceFactory
    {
        public IJoinService CreateBaseJoinService(AppDbContext db)
        {
            return new LeftJoinService(db, new CustomerTableWriter());
        }
    }
}
