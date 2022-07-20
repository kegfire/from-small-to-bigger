using SomeRepository;

namespace SomeService
{
    public class BooksService : IBooksService
    {
        private readonly IMongoDbRepository _dbRepository;
        private readonly ICacheService _cacheService;

        public BooksService(IMongoDbRepository dbRepository, ICacheService cacheService)
        {
            _dbRepository = dbRepository;
            _cacheService = cacheService;
        }

        public async Task CreateAsync(DbBook newBook)
        {
            await _dbRepository.Books.CreateAsync(newBook);
            await _cacheService.SetValueAsync(newBook.Id, newBook);
        }

        public Task<List<DbBook>> GetAsync()
        {
            return _dbRepository.Books.GetAsync();
        }

        public async Task<DbBook> GetAsync(string id)
        {
            var book = await _cacheService.GetValueAsync<DbBook>(id);
            return book ?? await _dbRepository.Books.GetAsync(id);
        }

        public async Task RemoveAsync(string id)
        {
            await _cacheService.DeleteValueAsync<DbBook>(id);
            await _dbRepository.Books.RemoveAsync(id);
        }

        public async Task UpdateAsync(DbBook updatedBook)
        {
            await _cacheService.UpdateValueAsync(updatedBook.Id, updatedBook);
            await _dbRepository.Books.UpdateAsync(updatedBook);
        }
    }
}