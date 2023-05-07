﻿using LibraryManagement.Core.Helpers.UserHelpers;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Service.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Add(Core.Entities.User user)
    {
        UserServiceHelper.CheckArgsForException(user);

        var doesExists = await _userRepository.GetByUsername(user.Username) != null;

        if (doesExists)
            throw new ArgumentException("This username already exists.");

        await _userRepository.Create(user);
    }

    public async Task Update(Core.Entities.User user)
    {
        UserServiceHelper.CheckArgsForException(user);

        var doesExists = await _userRepository.GetByUsername(user.Username) != null;

        if (!doesExists)
            throw new ArgumentException("User does not exists. Please check the entity.");

        await _userRepository.Update(user);
    }

    public async Task Delete(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Please enter a valid id.");

        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            throw new ArgumentException("User does not exists. Please check the entity.");

        await _userRepository.Delete(id);
    }

    public async Task<Core.Entities.User?> GetByUsername(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Core.Entities.User?> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<Core.Entities.User?> GetByPhone(string phone)
    {
        throw new NotImplementedException();
    }
}