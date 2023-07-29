using WorkTree.Business.Interface;
using WorkTree.Database.Models;
using WorkTree.Database.Models.Tree;
using WorkTree.Repositories.Interface;

namespace WorkTree.Business
{
    public class BaseItemBLL : IBaseItemBLL
    {
        #region Item

        private readonly IBaseItemRepository _baseItemRepository;

        public BaseItemBLL(IBaseItemRepository baseItemRepository)
        {
            _baseItemRepository = baseItemRepository;
        }

        public Task<IEnumerable<BaseItem>> GetAll()
        {
            return _baseItemRepository.GetAll();
        }

        public Task<BaseItem> Get(Guid id)
        {
            return _baseItemRepository.Get(id);
        }

        public Guid Insert(BaseItem baseItem)
        {
            return _baseItemRepository.Insert(baseItem);
        }

        public void Update(BaseItem baseItem)
        {
            _baseItemRepository.Update(baseItem);
        }

        public void Delete(Guid id)
        {
            _baseItemRepository.Delete(id);
        }

        #endregion Item

        #region ItemRelation

        public Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id)
        {
            return _baseItemRepository.GetAllItemRelation(id);
        }

        public Task<BaseItemRelation> GetItemRelation(Guid id)
        {
            return _baseItemRepository.GetItemRelation(id);
        }

        public Guid InsertItemRelation(BaseItemRelation baseItemRelation)
        {
            return _baseItemRepository.InsertItemRelation(baseItemRelation);
        }

        public void UpdateItemRelation(BaseItemRelation baseItemRelation)
        {
            _baseItemRepository.UpdateItemRelation(baseItemRelation);
        }

        public void DeleteItemRelation(Guid id)
        {
            _baseItemRepository.DeleteItemRelation(id);
        }

        public IBaseItemRepository Get_baseItemRepository()
        {
            return _baseItemRepository;
        }

        #endregion ItemRelation

        #region Build Tree

        public TreeBaseItemRelation BuildTree(Guid referenceItemId,
                                              bool includeParentsAndChildren = false,
                                              bool excludeChildrenFromParents = false)
        {
            TreeBaseItemRelation rootItem = _baseItemRepository.GetItemRelationTree(referenceItemId);

            return BuildTreeRecursive(rootItem, includeParentsAndChildren, excludeChildrenFromParents);
        }

        private TreeBaseItemRelation BuildTreeRecursive(TreeBaseItemRelation currentItem, bool includeParentsAndChildren, bool excludeChildrenFromParents)
        {
            if (currentItem == null)
                return null;

            TreeBaseItemRelation clonedItem = new TreeBaseItemRelation
            {
                Id = currentItem.Id,
                ParentId = currentItem.ParentId,
                Name = currentItem.Name,
                Image = currentItem.Image,
                ItemTypeId = currentItem.ItemTypeId,
                StartDate = currentItem.StartDate,
                EndDate = currentItem.EndDate,
                ItemStatusId = currentItem.ItemStatusId,
                OwnerTypeId = currentItem.OwnerTypeId,
                OwnerId = currentItem.OwnerId,
                ItemOrder = currentItem.ItemOrder,
                Parent = currentItem.Parent,
                Children = currentItem.Children
            };

            //if (includeParentsAndChildren){
                
                if (currentItem.ParentId.HasValue)
                {
                    TreeBaseItemRelation parentItem = _baseItemRepository.GetItemRelationTree(currentItem.ParentId.Value);
                    clonedItem.Parent = BuildTreeRecursive(parentItem, true, excludeChildrenFromParents);
                }

                //if (excludeChildrenFromParents){
                    
                    var children = _baseItemRepository.GetItemRelationTreeChildren(currentItem.Id);
                    foreach (var child in children)
                    {
                        clonedItem.Children.Add(BuildTreeRecursive(child, true, false));
                    }
                //}
            //}

            return clonedItem;
        }

        #endregion Build Tree
    }
}