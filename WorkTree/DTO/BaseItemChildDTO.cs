﻿namespace WorkTree.DTO
{
    public abstract class BaseItemChildDTO
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }

        //----------------------------------------
        public Guid OwnerType { get; set; }

        public Guid OwnerId { get; set; }
        public int Order { get; set; }
    }
}