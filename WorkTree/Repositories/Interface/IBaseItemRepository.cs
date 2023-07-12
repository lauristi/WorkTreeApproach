using WorkTree.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IBaseItemRepository
    {
        BaseItem Get(Guid id);

        void Insert(BaseItem baseItem);

        void Update(BaseItem baseItem);

        void Delete(Guid id);
    }
}