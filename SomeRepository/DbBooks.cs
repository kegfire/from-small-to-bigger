using MongoDB.Driver;

namespace SomeRepository
{
    public class DbBooks : IDbBooks
    {
        private readonly IMongoCollection<DbBook> _booksCollection;
        public DbBooks(MongoDbSettings settings)
        {
            var mongoClient = new MongoClient(settings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<DbBook>(settings.CollectionName);
        }

        public async Task<List<DbBook>> GetAsync() => await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<DbBook> GetAsync(string id) => await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DbBook newBook) => await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(DbBook updatedBook) => await _booksCollection.ReplaceOneAsync(x => x.Id == updatedBook.Id, updatedBook);

        public async Task RemoveAsync(string id) => await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
