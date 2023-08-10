namespace WorkTree.Database.Models.Tree
{
    public class TreeBuilderOptions
    {
        public Guid ReferenceItemId { get; set; }
        public bool BuildAscendentTree { get; set; }
        public bool IncludeAllAscendentChildren { get; set; }
    }
}