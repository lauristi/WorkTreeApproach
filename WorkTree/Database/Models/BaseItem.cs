using WorkTree.Database.Models.Base;

namespace WorkTree.Database.Models
{
    public class BaseItem : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        //----------------------------------------
        public Guid ItemTypeId { get; set; }

        public Guid OwnerTypeId { get; set; }
        public Guid OwnerId { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(this.Name);
        }
    }
}