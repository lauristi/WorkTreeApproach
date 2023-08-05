using WorkTree.Database.Models;
using WorkTree.Database.Models.Tree;

namespace WorkTree.Repositories.Interface
{
    public interface IBaseItemRepository
    {
        #region Item

        Task<BaseItem> Get(Guid id);

        Task<IEnumerable<BaseItem>> GetAll();

        Guid Insert(BaseItem baseItem);

        void Update(BaseItem baseItem);

        void Delete(Guid id);

        #endregion Item

        #region ItemRelation

        Task<BaseItemRelation> GetItemRelation(Guid id);

        Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id);

        Guid InsertItemRelation(BaseItemRelation baseItemRelation);

        void UpdateItemRelation(BaseItemRelation baseItemRelation);

        void DeleteItemRelation(Guid id);

        #endregion ItemRelation

        #region ItemRelationTree

        TreeBaseItemRelation GetItemRelationTree(Guid id);

        IEnumerable<TreeBaseItemRelation> GetItemRelationTreeChildren(Guid parentId);

        Guid GetParentOf(Guid id);

        #endregion ItemRelationTree
    }
}