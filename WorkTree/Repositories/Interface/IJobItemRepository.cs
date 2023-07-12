using WorkTree.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IJobItemRepository
    {
        JobItem Get(Guid id);

        void Insert(JobItem jobItem);

        void Update(JobItem jobItem);

        void Delete(Guid id);
    }
}