 namespace WorkTree.Database.DTO.Request
{
    public sealed class JobItemChildRequestDTO
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }

        //-------------------------------------
        private DateTime Start { get; set; }

        private DateTime End { get; set; }
        public Guid ItemStatus { get; set; }

        //-------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }

        public JobItemChildRequestDTO()
        {
                
        }
    }
}