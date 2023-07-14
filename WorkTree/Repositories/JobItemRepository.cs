using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkTree.Database.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Repositories
{
    public class JobItemRepository : IJobItemRepository
    {
        private readonly string connectionString;

        public JobItemRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("SqlServer");
        }

        public async Task<IEnumerable<JobItem>> GetAll()
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<JobItem>("SELECT * FROM JobItems");
        }

        public async Task<JobItem> Get(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<JobItem>("SELECT * FROM JobItems WHERE Id = @Id", new { Id = id });
        }

        public Guid Insert(JobItem jobItem)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            jobItem.Id = Guid.NewGuid();

            var parameters = new
            {
                Id = jobItem.Id,
                Name = jobItem.Name,
                Image = jobItem.Image,
                Start = jobItem.Start,
                End = jobItem.End,
                ItemStatus = jobItem.ItemStatus,
                ItemType = jobItem.ItemType,
                OwnerType = jobItem.OwnerType,
                OwnerId = jobItem.OwnerId
            };

            var sql = "INSERT INTO JobItems (Id, Name, Image, Start, End, ItemStatus, ItemType, OwnerType, OwnerId) " +
                        "VALUES (@Id, @Name, @Image, @Start, @End, @ItemStatus, @ItemType, @OwnerType, @OwnerId)";

            connection.Execute(sql, parameters);

            return jobItem.Id;
        }

        public void Update(JobItem jobItem)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = jobItem.Id,
                Name = jobItem.Name,
                Image = jobItem.Image,
                Start = jobItem.Start,
                End = jobItem.End],
                ItemStatus = jobItem.ItemStatus,
                ItemType = jobItem.ItemType,
                OwnerType = jobItem.OwnerType,
                OwnerId = jobItem.OwnerId
            };

            var sql = "UPDATE JobItems SET Name = @Name, Image = @Image, Start = @Start, " +
                        "End = @End, ItemStatus = @ItemStatus, ItemType = @ItemType, OwnerType = @OwnerType, OwnerId = @OwnerId " +
                        "WHERE Id = @Id";

            connection.Execute(sql, parameters);
        }

        public void Delete(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var sql = "DELETE FROM JobItems WHERE Id = @Id";

            connection.Execute(sql, new { Id = id });
        }

        #region Child

        public async Task<IEnumerable<JobItemChild>> GetAllChild()
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<JobItemChild>("SELECT * FROM JobItemChildren");
        }

        public async Task<JobItemChild> GetChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var sql = "SELECT * FROM JobItemChildren WHERE Id = @Id";

            return await connection.QuerySingleOrDefaultAsync<JobItemChild>(sql, new { Id = id });
        }

        public Guid InsertChild(JobItemChild jobItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            jobItemChild.Id = Guid.NewGuid();

            var parameters = new
            {
                Id = jobItemChild.Id,
                JobId = jobItemChild.JobId,
                Start = jobItemChild.Start,
                End = jobItemChild.End,
                ItemStatus = jobItemChild.ItemStatus,
                OwnerType = jobItemChild.OwnerType,
                OwnerId = jobItemChild.OwnerId,
                Order = jobItemChild.Order
            };

            var sql = "INSERT INTO JobItemChildren (Id, JobId, Start, End, ItemStatus, OwnerType, OwnerId, [Order]) " +
                        "VALUES (@Id, @JobId, @Start, @End, @ItemStatus, @OwnerType, @OwnerId, @Order)";

            connection.Execute(sql, parameters);

            return jobItemChild.Id;
        }

        public void UpdateChild(JobItemChild jobItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = jobItemChild.Id,
                JobId = jobItemChild.JobId,
                Start = jobItemChild.Start,
                End = jobItemChild.End,
                ItemStatus = jobItemChild.ItemStatus,
                OwnerType = jobItemChild.OwnerType,
                OwnerId = jobItemChild.OwnerId,
                Order = jobItemChild.Order
            };

            var sql = "UPDATE JobItemChildren SET JobId = @JobId, Start = @Start, " +
                        "End = @End, ItemStatus = @ItemStatus, OwnerType = @OwnerType, OwnerId = @OwnerId, [Order] = @Order " +
                        "WHERE Id = @Id";

            connection.Execute(sql, parameters);
        }

        public void DeleteChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var sql = "DELETE FROM JobItemChildren WHERE Id = @Id";

            connection.Execute(sql, new { Id = id });
        }

        #endregion Child
    }
}