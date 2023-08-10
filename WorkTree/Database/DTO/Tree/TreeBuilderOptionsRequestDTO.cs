namespace WorkTree.Database.DTO.Request
{
    public class TreeBuilderOptionsRequestDTO
    {
        public Guid ReferenceItemId { get; set; }
        public bool BuildAscendentTree { get; set; }
        public bool IncludeAllAscendentChildren { get; set; }
    }
}