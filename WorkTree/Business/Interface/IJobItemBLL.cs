using WorkTree.Database.Models;

namespace WorkTree.Business.Interface
{
    public interface IJobItemBLL
    {
        Task<IEnumerable<JobItem>> GetAll();

        Task<JobItem> Get(Guid id);

        Guid Insert(JobItem jobItem);

        void Update(JobItem jobItem);

        void Delete(Guid id);

        //--------------------------------------

        Task<IEnumerable<JobItemChild>> GetAllChild(Guid id);

        Task<JobItemChild> GetChild(Guid id);

        Guid InsertChild(JobItemChild jobItemChild);

        void UpdateChild(JobItemChild jobItemChild);

        void DeleteChild(Guid id);
    }
}