using EntityFrameworkExample.Core;

namespace EntityFrameworkExample.Services
{
    public abstract class BaseJoinService<IModel>: IJoinService where IModel: class
    {
        protected AppDbContext _db;
        protected BaseTableWriter<IModel> _tableWriter;

        public BaseJoinService(AppDbContext db, BaseTableWriter<IModel> tableWriter)
        {
            _db = db;
            _tableWriter = tableWriter;
        }

        public abstract void Query();
        public abstract void Print();
    }
}
