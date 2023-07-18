namespace WorkTree.Database.DTO.Request
{
    public sealed class BaseItemRequestDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }

        //----------------------------------------
        public Guid ItemType { get; set; }

        public Guid OwnerType { get; set; }
        public Guid OwnerId { get; set; }

        public BaseItemRequestDTO()
        {
            
        }
    }
}