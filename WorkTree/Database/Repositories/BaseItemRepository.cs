using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkTree.Database.Dapper.Interface;
using WorkTree.Database.Models;
using WorkTree.Repositories.Interface;

namespace WorkTree.Repositories
{
    public class BaseItemRepository : IBaseItemRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private string connectionString;

        public BaseItemRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
            connectionString = _connectionStringProvider.GetConnectionString();
        }

        public async Task<IEnumerable<BaseItem>> GetAll()
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT * FROM BASEITEM";
            //------------------------------------------------------
            return await connection.QueryAsync<BaseItem>(sql);
        }

        public async Task<BaseItem> Get(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT * FROM BASEITEM
                        WHERE Id = @Id";
            //------------------------------------------------------
            return await connection.QuerySingleOrDefaultAsync<BaseItem>(sql, new { Id = id });
        }

        public Guid Insert(BaseItem baseItem)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItem.Id,
                Name = baseItem.Name,
                Image = baseItem.Image,
                ItemType = baseItem.ItemType,
                OwnerType = baseItem.OwnerType,
                OwnerId = baseItem.OwnerId
            };

            String sql = @"INSERT INTO BASEITEM (Id,
                                             Name,
                                             Image,
                                             ItemType,
                                             OwnerType,
                                             OwnerId)
                              VALUES(@Id, @Name, @Image, @ItemType, @OwnerType, @OwnerId)";
            //-----------------------------------------------------------------
            connection.Execute(sql, parameters);

            return baseItem.Id;
        }

        public void Update(BaseItem baseItem)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItem.Id,
                Name = baseItem.Name,
                Image = baseItem.Image,
                ItemType = baseItem.ItemType,
                OwnerType = baseItem.OwnerType,
                OwnerId = baseItem.OwnerId
            };

            string sql = @"UPDATE BASEITEM SET Name = @Name,
                                           Image = @Image,
                                           ItemType = @ItemType,
                                           OwnerType = @OwnerType,
                                           OwnerId = @OwnerId
                                     WHERE Id = @Id";
            //------------------------------------------------------
            connection.Execute(sql, parameters);
        }

        public void Delete(Guid id)
        {
            string sql = "DELETE FROM BASEITEM WHERE Id = @Id";
            //------------------------------------------------------
            using IDbConnection connection = new SqlConnection(connectionString);
            connection.Execute(sql, new { Id = id });
        }

        #region Child

        public async Task<IEnumerable<BaseItemChild>> GetAllChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                             FROM BASEITEMCHILD
                            WHERE ParentId ={@id}";
            //------------------------------------------------------
            return await connection.QueryAsync<BaseItemChild>(sql);
        }

        public async Task<BaseItemChild> GetChild(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                             FROM BASEITEMCHILD
                            WHERE Id = @Id";
            //------------------------------------------------------
            return await connection.QuerySingleOrDefaultAsync<BaseItemChild>(sql, new { Id = id });
        }

        public Guid InsertChild(BaseItemChild baseItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItemChild.Id,
                ParentId = baseItemChild.ParentId,
                ChildId = baseItemChild.ChildId,
                OwnerType = baseItemChild.OwnerType,
                OwnerId = baseItemChild.OwnerId,
                Order = baseItemChild.Order
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

            return baseItemChild.Id;
        }

        public void UpdateChild(BaseItemChild baseItemChild)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItemChild.Id,
                ParentId = baseItemChild.ParentId,
                ChildId = baseItemChild.ChildId,
                OwnerType = baseItemChild.OwnerType,
                OwnerId = baseItemChild.OwnerId,
                Order = baseItemChild.Order
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