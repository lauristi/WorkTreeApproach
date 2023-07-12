using WorkTree.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IBaseItemChildRepository
    {
        BaseItemChild Get(Guid id);

        void Insert(BaseItemChild baseItemChild);

        void Update(BaseItemChild baseItemChild);

        void Delete(Guid id);
    }
}