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

        #region Child

        public Task<IEnumerable<BaseItemChild>> GetAllChild(Guid id)
        {
            return _baseItemRepository.GetAllChild(id);
        }

        public Task<BaseItemChild> GetChild(Guid id)
        {
            return _baseItemRepository.GetChild(id);
        }

        public Guid InsertChild(BaseItemChild baseItemChild)
        {
            return _baseItemRepository.InsertChild(baseItemChild);
        }

        public void UpdateChild(BaseItemChild baseItemChild)
        {
            _baseItemRepository.UpdateChild(baseItemChild);
        }

        public void DeleteChild(Guid id)
        {
            _baseItemRepository.DeleteChild(id);
        }

        #endregion Child
    }
}