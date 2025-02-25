namespace Model.Entity
{
    public class Reaction
    {
        public string Type { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
