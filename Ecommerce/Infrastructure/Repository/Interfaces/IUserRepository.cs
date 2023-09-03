﻿using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(UserAggregate user);
        Task<UserAggregate> GetUserAggregate(UserAggregate user);
    }
}