namespace WorkTree.Database.Models.Tree
{
    public class TreeBuilderReference
    {
        public Guid OriginalParentId { get; set; }
        public Guid ReferenceItemId { get; set; }
        public bool includeAllAscendentChildren { get; set; }

        //--------------------------------------------------------
        public List<Guid> Parents { get; set; } = new List<Guid>();
    }
}