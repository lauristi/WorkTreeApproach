using WorkTree.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IJobItemChildRepository
    {
        JobItemChild Get(Guid id);

        void Insert(JobItemChild jobItemChild);

        void Update(JobItemChild jobItemChild);

        void Delete(Guid id);
    }
}