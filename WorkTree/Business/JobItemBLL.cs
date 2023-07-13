using WorkTree.Business.Interface;
using WorkTree.Database.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Business
{
    public class JobItemBLL : IJobItemBLL
    {
        private readonly IJobItemRepository _jobItemRepository;

        public JobItemBLL(IJobItemRepository jobItemRepository)
        {
            _jobItemRepository = jobItemRepository;
        }

        public Task<IEnumerable<JobItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<JobItem> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Insert(JobItem jobItem)
        {
            throw new NotImplementedException();
        }

        public void Update(JobItem jobItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #region Child

        public Task<IEnumerable<JobItemChild>> GetAllChild()
        {
            throw new NotImplementedException();
        }

        public Task<JobItemChild> GetChild(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid InsertChild(JobItemChild jobItemChild)
        {
            throw new NotImplementedException();
        }

        public void UpdateChild(JobItemChild jobItemChild)
        {
            throw new NotImplementedException();
        }

        public void DeleteChild(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion Child
    }
}