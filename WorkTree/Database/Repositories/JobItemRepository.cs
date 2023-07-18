using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkTree.Database.Dapper.Interface;
using WorkTree.Database.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Repositories
{
    public class JobItemRepository : IJobItemRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private string connectionString;

        public JobItemRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
            connectionString = _connectionStringProvider.GetConnectionString();
        }

        public async Task<IEnumerable<JobItem>> GetAll()
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT * FROM JobItems";
            //--------------------------------------
            return await connection.QueryAsync<JobItem>(sql);
        }

        public async Task<JobItem> Get(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                             FROM JobItems
                            WHERE Id = @Id";
            //--------------------------------------
            return await connection.QuerySingleOrDefaultAsync<JobItem>(sql, new { Id = id });
        }

        public Guid Insert(JobItem jobItem)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

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

            var sql = @"INSERT INTO JobItems (Id,
                                              Name,
                                              Image,
                                              Start,
                                              End,
                                              ItemStatus,
                                              ItemType,
                                              OwnerType,
                                              OwnerId)
                            VALUES (@Id, @Name, @Image, @Start, @End, @ItemStatus, @ItemType, @OwnerType, @OwnerId)";

            //--------------------------------------
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
                End = jobItem.End,
                ItemStatus = jobItem.ItemStatus,
                ItemType = jobItem.ItemType,
                OwnerType = jobItem.OwnerType,
                OwnerId = jobItem.OwnerId
            };

            var sql = @"UPDATE JobItems SET Name = @Name,
                                            Image = @Image,
                                            Start = @Start,
                                            End = @End,
                                            ItemStatus = @ItemStatus,
                                            ItemType = @ItemType,
                                            OwnerType = @OwnerType,
                                            OwnerId = @OwnerId
                                      WHERE Id = @Id";
            //-----------------------------------------------
            connection.Execute(sql, parameters);
        }

        public void Delete(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var sql = @"DELETE FROM JobItems
                         WHERE Id = @Id";
            //-----------------------------------------------
            connection.Execute(sql, new { Id = id });
        }
        
        #region Child

        public async Task<IEnumerable<JobItemChild>> GetAllChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                      FROM BASEITEMCHILD
                     WHERE ParentId ={@id}";
            //------------------------------------------------------
            return await connection.QueryAsync<JobItemChild>(sql);
        }

        public async Task<JobItemChild> GetChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                      FROM BASEITEMCHILD
                     WHERE Id = @Id";
            //------------------------------------------------------
            return await connection.QuerySingleOrDefaultAsync<JobItemChild>(sql, new { Id = id });
        }

        public Guid InsertChild(JobItemChild jobItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = jobItemChild.Id,
                ParentId = jobItemChild.ParentId,
                ChildId = jobItemChild.ChildId,
                OwnerType = jobItemChild.OwnerType,
                OwnerId = jobItemChild.OwnerId,
                Order = jobItemChild.Order
            };

            string sql = @"INSERT INTO BASEITEMCHILD (Id,
                                               parentId,
                                               childId
                                               ItemId,
                                               OwnerType,
                                               OwnerId,
                                               [Order])
                                   VALUES (@Id,@ParentId,@ChildId, @ItemId, @OwnerType, @OwnerId, @Order)";
            //------------------------------------------------------
            connection.Execute(sql, parameters);

            return jobItemChild.Id;
        }

        public void UpdateChild(JobItemChild jobItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = jobItemChild.Id,
                ParentId = jobItemChild.ParentId,
                ChildId = jobItemChild.ChildId,
                OwnerType = jobItemChild.OwnerType,
                OwnerId = jobItemChild.OwnerId,
                Order = jobItemChild.Order
            };

            string sql = @"UPDATE BASEITEMCHILD SET ParentId = @ParentId,
                                              ChildId = @ChildId,
                                              OwnerType = @OwnerType,
                                              OwnerId = @OwnerId,
                                              [Order] = @Order
                                       WHERE Id = @Id";
            //------------------------------------------------------
            connection.Execute(sql, parameters);
        }

        public void DeleteChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"DELETE FROM BASEITEMCHILD
                      WHERE Id = @Id";
            //------------------------------------------------------
            connection.Execute(sql, new { Id = id });
        }

        #endregion Child

    }
}