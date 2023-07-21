using WorkTree.Database.Models;

namespace WorkTree.Business.Interface
{
    public interface IBaseItemBLL
    {
        Task<IEnumerable<BaseItem>> GetAll();

        Task<BaseItem> Get(Guid id);

        Guid Insert(BaseItem baseItem);

        void Update(BaseItem baseItem);

        void Delete(Guid id);

        //-------------------------------------------------

        Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id);

        Task<BaseItemRelation> GetItemRelation(Guid id);

        Guid InsertItemRelation(BaseItemRelation baseItemRelation);

        void UpdateItemRelation(BaseItemRelation baseItemRelation);

        void DeleteItemRelation(Guid id);
    }
}