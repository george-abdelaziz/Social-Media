using Model.Entity;

namespace DataAccess.Interface
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        public Task<ApplicationUser?> Update(ApplicationUser applicationUser);
    }
}
