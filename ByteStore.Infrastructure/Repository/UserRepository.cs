﻿using ByteStore.Domain.Aggregates;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure;
using ByteStore.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public UserRepository(AppDbContext context, IShoppingCartRepository shoppingCartRepository)
        {
            _context = context;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task RegisterUser(UserAggregate userAggregate)
        {
            await _context.UserAggregates.AddAsync(userAggregate);
            await _context.SaveChangesAsync();
            await _shoppingCartRepository.CreateShoppingCart(userAggregate.UserAggregateId);
        }

        public async Task<UserAggregate> GetUserAggregate(UserAggregate user)
        {
            return await _context.UserAggregates
                .AsNoTracking()
                .Include(x => x.User).FirstOrDefaultAsync(u => u.User.Username == user.User.Username);
        }

        public async Task EditUserAddress(Address address, int userId)
        {
            var user = await _context.UserAggregates.FirstOrDefaultAsync(u => u.UserAggregateId == userId);
            if (user == null)
            {
                return;
            }
            user.Address = address;
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetUserAddress(int userId)
        {
            var user = await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.UserAggregateId == userId);
            return user == null ? null : user.Address;
        }


        //public async Task<User> GetUserByUsername() {
        //    await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.User.Username);
        //}
    }
}
