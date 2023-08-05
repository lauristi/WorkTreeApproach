namespace WorkTree.Database.Models.Tree
{
    public class TreeBuilderOptions
    {
        public Guid ReferenceItemId { get; set; }
        public bool buildAscendentTree { get; set; }
        public bool includeAllAscendentChildren { get; set; }
    }
}