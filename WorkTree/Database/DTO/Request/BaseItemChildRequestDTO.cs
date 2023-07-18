namespace WorkTree.Database.DTO.Request
{
    public sealed class BaseItemChildRequestDTO
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }

        //----------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }

        public BaseItemChildRequestDTO()
        {
            
        }

    }
}