using WorkTree.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Repositories
{
    public class BaseItemChildRepository : IBaseItemChildRepository
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public BaseItemChild Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(BaseItemChild baseItemChild)
        {
            throw new NotImplementedException();
        }

        public void Update(BaseItemChild baseItemChild)
        {
            throw new NotImplementedException();
        }
    }
}