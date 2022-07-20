namespace SomeRepository
{
    public interface IDbBooks
    {
        Task CreateAsync(DbBook newBook);
        Task<List<DbBook>> GetAsync();
        Task<DbBook> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(DbBook updatedBook);
    }
}