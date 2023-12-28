﻿using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;

        private IAppUserRepository<AppUser>? _accountRepository;
        private IRoleRepository<IdentityRole>? _roleRepository;
        private IUserRoleRepository<IdentityUserRole<long>>? _userRoleRepository;

        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
        }

        public IAppUserRepository<AppUser> Users
        {

            get
            {
                _accountRepository ??= new AppUserRepository(_context);
                return _accountRepository;
            }
        }

        public IRoleRepository<IdentityRole> Roles
        {

            get
            {
                _roleRepository ??= new RoleRepository(_context);
                return _roleRepository;
            }
        }

        public IUserRoleRepository<IdentityUserRole<long>> UserRoles
        {

            get
            {
                _userRoleRepository ??= new UserRoleRepository(_context);
                return _userRoleRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
