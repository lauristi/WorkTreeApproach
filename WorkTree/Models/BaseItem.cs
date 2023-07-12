﻿namespace WorkTree.Models
{
    public abstract class BaseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        //----------------------------------------
        public Guid ItemType { get; set; }

        public Guid OwnerType { get; set; }
        public Guid OwnerId { get; set; }
    }
}