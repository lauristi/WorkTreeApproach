using WorkTree.Database.Models.Base;

namespace WorkTree.Database.Models
{
    public class JobItem : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        //----------------------------------------
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        public Guid ItemStatus { get; set; }

        //----------------------------------------
        public Guid ItemType { get; set; }

        public Guid OwnerType { get; set; }
        public Guid OwnerId { get; set; }

        public JobItem()
        {
            this.Start = DateTime.Now;
            this.End = DateTime.Now.AddMonths(3);
        }
    }
}