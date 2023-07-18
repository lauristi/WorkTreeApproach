using WorkTree.Database.Models.Base;

namespace WorkTree.Database.Models
{
    public class BaseItemChild : BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }

        //----------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }
    }
}