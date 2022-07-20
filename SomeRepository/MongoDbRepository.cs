namespace SomeRepository
{
    public class MongoDbRepository : IMongoDbRepository
    {
        public MongoDbRepository(MongoDbSettings settings)
        {
            Books = new DbBooks(settings);
        }

        public IDbBooks Books { get; private set; }
    }
}