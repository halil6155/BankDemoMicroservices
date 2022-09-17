using AutoMapper;
using BanksDemo.Shared.Events.Concrete;
using BanksDemo.User.Contexts;
using BanksDemo.User.DTOs;
using BanksDemo.User.Repositories.Abstract;
using MassTransit;
using MongoDB.Driver;


namespace BanksDemo.User.Repositories.Concrete;

public class UserRepository : IUserRepository
{
    private readonly IUserContext _userContext;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public UserRepository(IUserContext userContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _userContext = userContext;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<IEnumerable<UserForListDto>> GetAllAsync()
    {
        var userList = await _userContext.Users.Find(x => true).ToListAsync();
        return _mapper.Map<IEnumerable<UserForListDto>>(userList);
    }

    public async Task<bool> DeleteAsync(string userId)
    {
        var filter = Builders<Models.User>.Filter.Eq(x => x.Id, userId);
        var deleteResult = await _userContext.Users.DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<bool> CreateAsync(UserForRegisterDto userForRegisterDto)
    {
        var user = _mapper.Map<Models.User>(userForRegisterDto);
        user.CreatedOn = DateTime.Now;
        user.IsActive = true;
        await _userContext.Users.InsertOneAsync(user);
        await _publishEndpoint.Publish(new UserCreatedEvent(user.FirstName, user.LastName, user.Id));
        return true;
    }

    public async Task<UserForListDto?> GetByIdAsync(string id)
    {
        var user = await _userContext.Users.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<UserForListDto>(user);
    }
}