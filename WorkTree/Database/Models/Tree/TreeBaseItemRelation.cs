namespace WorkTree.Database.Models.Tree
{
    public class TreeBaseItemRelation
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Guid ItemTypeId { get; set; }

        //----------------------------------------
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public Guid ItemStatusId { get; set; }

        //----------------------------------------
        public Guid OwnerTypeId { get; set; }

        public Guid OwnerId { get; set; }

        //----------------------------------------
        public int ItemOrder { get; set; }

        // Pai e Filhos para montagem hieraquica
        public TreeBaseItemRelation Parent { get; set; }

        public List<TreeBaseItemRelation> Children { get; set; } = new List<TreeBaseItemRelation>();
    }
}