using WorkTree.Database.Models;

namespace WorkTree.Repositories.Interface
{
    public interface IJobItemRepository
    {
        Task<JobItem> Get(Guid id);

        Task<IEnumerable<JobItem>> GetAll();

        Guid Insert(JobItem jobItem);

        void Update(JobItem jobItem);

        void Delete(Guid id);

        //--------------------------------------

        Task<JobItemChild> GetChild(Guid id);

        Task<IEnumerable<JobItemChild>> GetAllChild(Guid id);

        Guid InsertChild(JobItemChild jobItemChild);

        void UpdateChild(JobItemChild jobItemChild);

        void DeleteChild(Guid id);
    }
}