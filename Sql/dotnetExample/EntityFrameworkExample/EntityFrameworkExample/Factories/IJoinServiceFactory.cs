using System;
using EntityFrameworkExample.Core;
using EntityFrameworkExample.Services;

namespace EntityFrameworkExample.Factories
{
    public interface IJoinServiceFactory
    {
        IJoinService CreateBaseJoinService(AppDbContext db);
    }
}
