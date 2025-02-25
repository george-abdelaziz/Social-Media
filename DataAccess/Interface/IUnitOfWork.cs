using Model.Entity;

namespace DataAccess.Interface
{
    public interface IUnitOfWork
    {
        public IApplicationUserRepository ApplicationUser { get; }
        public Task<ApplicationUser> DeleteApplicationUserAsync(int applicationUserId);
        public Task<ApplicationUser> GetApplicationUser(int applicationUserId);
        Task SaveAsync();
    }
}
