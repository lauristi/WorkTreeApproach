namespace WorkTree.Database.Models
{
    public abstract class JobItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        //----------------------------------------
        private DateTime Start { get; set; }

        private DateTime End { get; set; }
        public Guid ItemStatus { get; set; }

        //----------------------------------------
        public Guid ItemType { get; set; }

        public Guid OwnerType { get; set; }
        public Guid OwnerId { get; set; }
    }
}