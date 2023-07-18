using WorkTree.Database.Models.Base;

namespace WorkTree.Database.Models
{
    public class JobItemChild : BaseEntity
    {
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }

        //-------------------------------------
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        public Guid ItemStatus { get; set; }

        //-------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }

        public JobItemChild()
        {
            this.Start = DateTime.Now;
            this.End = DateTime.Now.AddMonths(3);
        }
    }
}