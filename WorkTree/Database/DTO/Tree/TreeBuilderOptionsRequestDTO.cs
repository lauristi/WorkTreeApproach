namespace WorkTree.Database.DTO.Request
{
    public class TreeBuilderOptionsRequestDTO
    {
        public Guid ReferenceItemId { get; set; }
        public bool buildAscendentTree { get; set; }
        public bool includeAllAscendentChildren { get; set; }
    }
}