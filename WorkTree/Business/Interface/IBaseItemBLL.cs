using WorkTree.Business.TreeBase;
using WorkTree.Database.Models;

namespace WorkTree.Business.Interface
{
    public interface IBaseItemBLL
    {
        #region CRUD

        Task<IEnumerable<BaseItem>> GetAll();

        Task<BaseItem> Get(Guid id);

        Guid Insert(BaseItem baseItem);

        void Update(BaseItem baseItem);

        void Delete(Guid id);

        #endregion CRUD

        #region CRUD Relation

        Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id);

        Task<BaseItemRelation> GetItemRelation(Guid id);

        Guid InsertItemRelation(BaseItemRelation baseItemRelation);

        void UpdateItemRelation(BaseItemRelation baseItemRelation);

        void DeleteItemRelation(Guid id);

        #endregion CRUD Relation

        #region TreeBuilder

        TreeBaseItemRelation BuildTree(Guid itemId,
                               bool includeParentsAndChildren = false,
                               bool excludeChildrenFromParents = false);

        #endregion TreeBuilder
    }
}