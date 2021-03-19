namespace Logic.AppServices.Queries
{
    public interface IQueryHandler<TQuery, TResult> : IQuery<TResult>
    {
        TResult Handle(TQuery command);
    }
}