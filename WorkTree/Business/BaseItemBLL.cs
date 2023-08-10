using WorkTree.Business.Interface;
using WorkTree.Database.Models;
using WorkTree.Database.Models.Tree;
using WorkTree.Repositories.Interface;

namespace WorkTree.Business
{
    public class BaseItemBLL : IBaseItemBLL
    {
        #region Item CRUD

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

        #region ItemRelation CRUD

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

        public TreeBaseItemRelation TreeBuilder(TreeBuilderOptions treeBuilderOptions)
        {
            TreeBuilderReference treeBuilderReference = new TreeBuilderReference();
            TreeBaseItemRelation referenceItem = new TreeBaseItemRelation();

            treeBuilderReference.ReferenceItemId = treeBuilderOptions.ReferenceItemId;
            treeBuilderReference.includeAllAscendentChildren = treeBuilderOptions.IncludeAllAscendentChildren;

            //Monta arvore completa desde o pai Original
            if (treeBuilderOptions.BuildAscendentTree)
            {
                treeBuilderReference.OriginalParentId = treeBuilderOptions.ReferenceItemId;
                treeBuilderReference = SearchOriginalParentRecursive(treeBuilderReference);

                //Recupero o item completo  do id do pai original. Ele será o ponto zero da montagem da arvore
                referenceItem = _baseItemRepository.GetItemRelationTree(treeBuilderReference.OriginalParentId);
            }
            else
            {
                //Recupero o item completo do id informado. Ele será o ponto zero da montagem da arvore
                referenceItem = _baseItemRepository.GetItemRelationTree(treeBuilderOptions.ReferenceItemId);
            }

            TreeBaseItemRelation completeTree = new TreeBaseItemRelation();

            completeTree = BuildTreeRecursive(referenceItem, treeBuilderReference);

            return completeTree;
        }

        private TreeBuilderReference SearchOriginalParentRecursive(TreeBuilderReference treeBuilderReference)
        {
            //-----------------------------------------------------------------------------------
            // Esta rotina recursiva realiza uma busca inversa de Parents, a patir do item
            // de referencia, até encontrar o pai original da estrutura
            //------------------------------------------------------------------------------------

            Guid currentParentId = _baseItemRepository.GetParentOf(treeBuilderReference.OriginalParentId);

            if (currentParentId == Guid.Empty)
            {
                treeBuilderReference.OriginalParentId = treeBuilderReference.OriginalParentId;
                treeBuilderReference.Parents.Add(currentParentId);
                return treeBuilderReference;
            }
            else
            {
                treeBuilderReference.OriginalParentId = currentParentId;
                treeBuilderReference.Parents.Add(currentParentId);
                return SearchOriginalParentRecursive(treeBuilderReference);
            }
        }

        private TreeBaseItemRelation BuildTreeRecursive(TreeBaseItemRelation currentItem, TreeBuilderReference treeBuilderReference)
        {
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

            var children = _baseItemRepository.GetItemRelationTreeChildren(currentItem.Id);
            foreach (var child in children)
            {
                //Sao condicoes para incluir um ramo filho
                // 01 Inclusao de todos os filhos dos ascentes marcada como true
                // 02 O filho encontrado constar na lista de parentes ascendentes
                // 03 O filho encontrado é o item de referencia
                // 04 O filho encontrado tem pai o item de referencia

                if (treeBuilderReference.includeAllAscendentChildren ||
                    treeBuilderReference.Parents.Contains(child.Id) ||
                    child.Id == treeBuilderReference.ReferenceItemId ||
                    child.ParentId == treeBuilderReference.ReferenceItemId
                    )
                {
                    clonedItem.Children.Add(BuildTreeRecursive(child, treeBuilderReference));
                }
            }

            return clonedItem;
        }
    }

    #endregion Build Tree
}