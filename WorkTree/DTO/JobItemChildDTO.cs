namespace WorkTree.DTO
{
    public abstract class JobItemChildDTO
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }

        //-------------------------------------
        private DateTime Start { get; set; }

        private DateTime End { get; set; }
        public Guid ItemStatus { get; set; }

        //-------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }
    }
}