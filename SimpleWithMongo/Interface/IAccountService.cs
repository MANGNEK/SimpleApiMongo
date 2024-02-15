using SimpleWithMongo.Model;
using SimpleWithMongo.Request;
using SimpleWithMongo.Response;

namespace SimpleWithMongo.Interface;

public interface IAccountService
{
    Task Create(AccountResquest account);
    Task DeleteByIdAsync(string id);
    Task Update(string Id, AccountResquest account);
    Task<List<AccountResponse>> GetAllAsync();
    Task<AccountResponse> GetByIdAsync(string id);
}
