using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entity
{
    [Table("ApplicationUsers")]
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ICollection<Content> Contents { get; set; } = new List<Content>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    }
}
