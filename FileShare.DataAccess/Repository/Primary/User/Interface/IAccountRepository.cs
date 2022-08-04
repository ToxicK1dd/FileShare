﻿using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Repository.Primary.Account.Interface
{
    public interface IUserRepository : IRepositoryBase<Model>
    {
        Task<bool> IsEnabledByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsVerifiedByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Model> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<Guid> GetIdByUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}