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
            throw new NotImplementedException();
        }

        public Task<BaseItem> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(BaseItem baseItem)
        {
            throw new NotImplementedException();
        }

        public void Update(BaseItem baseItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #region Child

        public Task<IEnumerable<BaseItemChild>> GetAllChild()
        {
            throw new NotImplementedException();
        }

        public Task<BaseItemChild> GetChild(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid InsertChild(BaseItemChild baseItemChild)
        {
            throw new NotImplementedException();
        }

        public void UpdateChild(BaseItemChild baseItemChild)
        {
            throw new NotImplementedException();
        }

        public void DeleteChild(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion Child
    }
}