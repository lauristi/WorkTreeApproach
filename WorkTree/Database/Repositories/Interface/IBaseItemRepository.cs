using WorkTree.Database.Models;

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
        Task<BaseItemChild> GetChild(Guid id);

        Task<IEnumerable<BaseItemChild>> GetAllChild(Guid id);

        Guid InsertChild(BaseItemChild baseItemChild);

        void UpdateChild(BaseItemChild baseItemChild);

        void DeleteChild(Guid id);
    }
}