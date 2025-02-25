using Microsoft.EntityFrameworkCore;

namespace Model.Entity
{
    public class Content
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;
        public int? ParentId { get; set; } = null;
        public Content? Parent { get; set; }
        public ICollection<Content> Comments { get; set; } = new List<Content>();
        public ICollection<Media> Media { get; set; } = new List<Media>();
    }
}
