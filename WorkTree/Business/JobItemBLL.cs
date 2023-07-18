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
            return _jobItemRepository.GetAll();
        }

        public Task<JobItem> Get(Guid id)
        {
            return _jobItemRepository.Get(id);
        }

        public Guid Insert(JobItem jobItem)
        {
            return _jobItemRepository.Insert(jobItem);
        }

        public void Update(JobItem jobItem)
        {
            _jobItemRepository.Update(jobItem);
        }

        public void Delete(Guid id)
        {
            _jobItemRepository.Delete(id);
        }

        #region Child

        public Task<IEnumerable<JobItemChild>> GetAllChild(Guid id)
        {
            return _jobItemRepository.GetAllChild(id);
        }

        public Task<JobItemChild> GetChild(Guid id)
        {
            return _jobItemRepository.GetChild(id);
        }

        public Guid InsertChild(JobItemChild jobItemChild)
        {
            return _jobItemRepository.InsertChild(jobItemChild);
        }

        public void UpdateChild(JobItemChild jobItemChild)
        {
            _jobItemRepository.UpdateChild(jobItemChild);
        }

        public void DeleteChild(Guid id)
        {
            _jobItemRepository.DeleteChild(id);
        }

        #endregion Child
    }
}