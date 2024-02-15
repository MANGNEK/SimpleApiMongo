using Mapster;
using MongoDB.Driver;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;
using SimpleWithMongo.Request;
using SimpleWithMongo.Response;

namespace SimpleWithMongo.Services;

public class AccountService : IAccountService
{
    private readonly IMongoCollection<Account> _accounts;   
    public AccountService(IMongoDbContext context)
    {
        _accounts = context.GetCollection<Account>("Account");
    }
    public async Task Create(AccountResquest account)
    {
       await _accounts.InsertOneAsync(account.Adapt<Account>());
    }
    public async Task<AccountResponse> GetByIdAsync(string id)
    {
        var data = await _accounts.Find(e => e.Id.ToString() == id).FirstOrDefaultAsync();

        return data.Adapt<AccountResponse>();
    }
    public async Task Update(string Id, AccountResquest account)
    {
        var filter = Builders<Account>.Filter.Eq(e => e.Id, Id);
        var existingAccount = await _accounts.Find(filter).FirstOrDefaultAsync();
        if (existingAccount != null)
        {
            var updatedAccount = account.Adapt<Account>();         
            var update = Builders<Account>.Update
                .Set(e => e.User, updatedAccount.User)
                .Set(e => e.Email, updatedAccount.Email)
                .Set(e => e.Password, updatedAccount.Password)
                .Set(e => e.Phone, updatedAccount.Phone)
                .Set(e => e.Name, updatedAccount.Name);
            await _accounts.UpdateOneAsync(filter, update);
        }
    }
    public async Task DeleteByIdAsync(string id)
    {
        var filter = Builders<Account>.Filter.Eq(e => e.Id, id);
        var delete = Builders<Account>.Update.Set(e => e.Status, "Inactive");
        await _accounts.UpdateOneAsync(filter, delete);
    }
    public async Task<List<AccountResponse>>GetAllAsync()
    {
        var data = await _accounts.Find(e => e.Status == "Active").ToListAsync();
        var responseList = data.Adapt<List<AccountResponse>>();
        return responseList;
    }
}
