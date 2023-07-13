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

        Task<IEnumerable<BaseItemChild>> GetAllChild();

        Task<BaseItemChild> GetChild(Guid id);

        Guid InsertChild(BaseItemChild baseItemChild);

        void UpdateChild(BaseItemChild baseItemChild);

        void DeleteChild(Guid id);
    }
}