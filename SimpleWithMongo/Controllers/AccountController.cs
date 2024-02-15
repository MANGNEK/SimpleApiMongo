using Mapster;
using Microsoft.AspNetCore.Mvc;
using SimpleWithMongo.Interface;
using SimpleWithMongo.Model;
using SimpleWithMongo.Request;
using SimpleWithMongo.Response;

namespace SimpleWithMongo.Controllers;

public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;
    public AccountController (IAccountService accountService)
    {
        this.accountService = accountService;
    }
    [HttpPost]
    [Route("create")]
    public async Task CreateAsync(AccountResquest data)
    {
        await accountService.Create(data);
    }
    [HttpGet]
    [Route("getAll")]
    public async Task<List<AccountResponse>> getAllAccount()
    {
        var data = await accountService.GetAllAsync();
       if(data.Count > 0) { 
        return data;
        }
       return new List<AccountResponse>();  
    }
    [HttpGet]
    [Route("getById")]
    public async Task<AccountResponse>GetById(string id)
    {
        return await accountService.GetByIdAsync(id);
    }
    [HttpPut]
    [Route("update")]
    public async void Update(string id , AccountResquest account)
    {
        await accountService.Update(id, account);
    }
    [HttpDelete]
    [Route("delete")]
    public async void delete(string id)
    {
        await accountService.DeleteByIdAsync(id);
    }
}
