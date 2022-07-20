using SomeRepository;

namespace SomeService
{
    public interface IBooksService
    {
        Task CreateAsync(DbBook newBook);
        Task<List<DbBook>> GetAsync();
        Task<DbBook> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(DbBook updatedBook);
    }
}