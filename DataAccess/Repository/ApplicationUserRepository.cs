using DataAccess.Interface;
using Model.Entity;

namespace DataAccess.Repository
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
        public Task<ApplicationUser?> Update(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
