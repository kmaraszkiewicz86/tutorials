using Logic.Students.Queries;

namespace Logic.Students.QueryHandlers
{
    public interface IQueryHandler<TQuery, TResult> : IQuery<TResult>
    {
        TResult Handle(TQuery command);
    }
}
