using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
        }
        public async Task<ApplicationUser> DeleteApplicationUserAsync(int applicationUserId)
        {
            var applicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == applicationUserId);
            if (applicationUser == null)
            {
                return null;
            }
            return applicationUser;
        }
        
        public async Task<ApplicationUser> GetApplicationUser(int applicationUserId)
        {
            var applicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == applicationUserId);
            if (applicationUser == null)
            {
                return null;
            }
            return applicationUser;
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
