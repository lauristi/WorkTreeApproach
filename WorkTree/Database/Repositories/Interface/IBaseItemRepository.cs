﻿using WorkTree.Database.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IBaseItemRepository
    {
        Task<BaseItem> Get(Guid id);

        Task<IEnumerable<BaseItem>> GetAll();

        Guid Insert(BaseItem baseItem);

        void Update(BaseItem baseItem);

        void Delete(Guid id);

        //-------------------------------------------------
        Task<BaseItemRelation> GetItemRelation(Guid id);

        Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id);

        Guid InsertItemRelation(BaseItemRelation baseItemRelation);

        void UpdateItemRelation(BaseItemRelation baseItemRelation);

        void DeleteItemRelation(Guid id);
    }
}