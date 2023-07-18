namespace WorkTree.Database.DTO.Response
{
    public sealed class JobItemChildResponseDTO
    {
        public Guid Id { get; set; }
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

        public JobItemChildResponseDTO()
        {
                
        }
    }
}