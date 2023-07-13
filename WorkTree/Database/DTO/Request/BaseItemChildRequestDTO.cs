namespace WorkTree.Database.DTO.Request
{
    public abstract class BaseItemChildRequestDTO
    {
        public Guid ItemId { get; set; }

        //----------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }
    }
}