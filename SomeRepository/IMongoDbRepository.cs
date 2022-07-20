namespace SomeRepository
{
    public interface IMongoDbRepository
    {
        IDbBooks Books { get; }
    }
}