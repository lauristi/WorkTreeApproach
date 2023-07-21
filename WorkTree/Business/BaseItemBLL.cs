using WorkTree.Business.Interface;
using WorkTree.Database.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Business
{
    public class BaseItemBLL : IBaseItemBLL
    {
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

        #endregion ItemRelation
    }
}