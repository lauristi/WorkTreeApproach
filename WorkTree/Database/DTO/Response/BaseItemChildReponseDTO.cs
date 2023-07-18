namespace WorkTree.Database.DTO.Response
{
    public sealed class BaseItemChildResponseDTO
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }

        //----------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }

        public BaseItemChildResponseDTO()
        {
                
        }
    }
}