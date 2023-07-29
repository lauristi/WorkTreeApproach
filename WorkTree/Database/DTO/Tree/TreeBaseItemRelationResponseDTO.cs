namespace WorkTree.Database.DTO.Response
{
    public class TreeBaseItemRelationResponseDTO
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
        public TreeBaseItemRelationResponseDTO Parent { get; set; }

        public List<TreeBaseItemRelationResponseDTO> Children { get; set; } = new List<TreeBaseItemRelationResponseDTO>();
    }
}