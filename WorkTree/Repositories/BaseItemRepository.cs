using WorkTree.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Repositories
{
    public class BaseItemRepository : IBaseItemRepository
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public BaseItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(BaseItem baseItem)
        {
            throw new NotImplementedException();
        }

        public void Update(BaseItem baseItem)
        {
            throw new NotImplementedException();
        }
    }
}