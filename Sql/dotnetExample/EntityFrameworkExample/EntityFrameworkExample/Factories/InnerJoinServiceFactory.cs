using System;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Models;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public class InnerJoinServiceFactory : IJoinServiceFactory
    {
        public IJoinService CreateBaseJoinService(AppDbContext db)
        {
            return new InnerJoinService(db, new CustomerTableWriter());
        }
    }
}
