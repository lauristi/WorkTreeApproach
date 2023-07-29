using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkTree.Database.Dapper.Interface;
using WorkTree.Database.Models;
using WorkTree.Database.Models.Tree;
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

        #region Item

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
                ItemType = baseItem.ItemTypeId,
                OwnerType = baseItem.OwnerTypeId,
                OwnerId = baseItem.OwnerId
            };

            String sql = @"INSERT INTO BASEITEM (Id,
                                             Name,
                                             Image,
                                             ItemTypeId,
                                             OwnerTypeId,
                                             OwnerId)
                              VALUES(@Id, @Name, @Image, @ItemTypeId, @OwnerTypeId, @OwnerId)";
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
                ItemType = baseItem.ItemTypeId,
                OwnerType = baseItem.OwnerTypeId,
                OwnerId = baseItem.OwnerId
            };

            string sql = @"UPDATE BASEITEM SET Name = @Name,
                                           Image = @Image,
                                           ItemTypeId = @ItemTypeId,
                                           OwnerTypeId = @OwnerTypeId,
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

        #endregion Item

        #region ItemRelation

        public async Task<IEnumerable<BaseItemRelation>> GetAllItemRelation(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                             FROM BASEITEMRELATION
                            WHERE ParentId ={@id}";
            //------------------------------------------------------
            return await connection.QueryAsync<BaseItemRelation>(sql);
        }

        public async Task<BaseItemRelation> GetItemRelation(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT *
                             FROM BASEITEMRELATION
                            WHERE Id = @Id";
            //------------------------------------------------------
            return await connection.QuerySingleOrDefaultAsync<BaseItemRelation>(sql, new { Id = id });
        }

        public Guid InsertItemRelation(BaseItemRelation baseItemRelation)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItemRelation.Id,
                ParentId = baseItemRelation.ParentId,
                Name = baseItemRelation.Name,
                Image = baseItemRelation.Image,
                ItemTypeId = baseItemRelation.ItemTypeId,
                StartDate = baseItemRelation.StartDate,
                EndDate = baseItemRelation.EndDate,
                ItemStatusId = baseItemRelation.ItemStatusId,
                OwnerTypeId = baseItemRelation.OwnerTypeId,
                OwnerId = baseItemRelation.OwnerId,
                ItemOrder = baseItemRelation.ItemOrder
            };

            string sql = @"INSERT INTO BASEITEMRELATION(
                                       ID,
                                       PARENTID,
                                       NAME,
                                       IMAGE,
                                       ITEMTYPEID,
                                       STARTDATE,
                                       ENDDATE,
                                       ITEMSTATUSID,
                                       OWNERTYPEID,
                                       OWNERID,
                                       ITEMORDER)
                                 VALUES(@Id,
                                        @ParentId,
                                        @Name,
                                        @Image,
                                        @ItemTypeId,
                                        @StartDate,
                                        @EndDate,
                                        @ItemStatusId,
                                        @OwnerTypeId,
                                        @OwnerId,
                                        @ItemOrder)";
            //------------------------------------------------------
            connection.Execute(sql, parameters);

            return baseItemRelation.Id;
        }

        public void UpdateItemRelation(BaseItemRelation baseItemRelation)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            var parameters = new
            {
                Id = baseItemRelation.Id,
                ParentId = baseItemRelation.ParentId,
                Name = baseItemRelation.Name,
                Image = baseItemRelation.Image,
                ItemTypeId = baseItemRelation.ItemTypeId,
                StartDate = baseItemRelation.StartDate,
                EndDate = baseItemRelation.EndDate,
                ItemStatusId = baseItemRelation.ItemStatusId,
                OwnerTypeId = baseItemRelation.OwnerTypeId,
                OwnerId = baseItemRelation.OwnerId,
                ItemOrder = baseItemRelation.ItemOrder
            };

            string sql = @"UPDATET BASEITEMRELATION SET
                                       ID=@Id,
                                       PARENTID=@ParentId,
                                       NAME=@Name,
                                       IMAGE=@Image,
                                       ITEMTYPEID=@ItemTypeId,
                                       STARTDATE=@StartDate,
                                       ENDDATE=@EndDate,
                                       ITEMSTATUSID=@ItemStatusId,
                                       OWNERTYPEID=@OwnerTypeId,
                                       OWNERID=@OwnerId,
                                       ITEMORDER=@ItemOrder)
                                 WHERE ID = @Id";
            //------------------------------------------------------
            connection.Execute(sql, parameters);
        }

        public void DeleteItemRelation(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"DELETE FROM BASEITEMRELATION
                                 WHERE ID = @Id";
            //------------------------------------------------------
            connection.Execute(sql, new { Id = id });
        }

        #endregion ItemRelation

        #region ItemRelationTree

        public TreeBaseItemRelation GetItemRelationTree(Guid id)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT Id,
                                  ParentId,
                                  Name,
                                  Image,
                                  ItemTypeId,
                                  StartDate,
                                  EndDate,
                                  ItemStatusId,
                                  OwnerTypeId,
                                  OwnerId,
                                  ItemOrder
                             FROM BASEITEMRELATION
                            WHERE Id = @Id";
            //--------------------------------------------------------------------------------
            var result = connection.QuerySingleOrDefault(sql, new { Id = id });

            TreeBaseItemRelation baseItemTree = new TreeBaseItemRelation();

            if (result == null)
            {
                return baseItemTree;
            }

            baseItemTree.Id = result.Id;
            baseItemTree.ParentId = result.ParentId;
            baseItemTree.Name = result.Name;
            baseItemTree.Image = result.Image;
            baseItemTree.ItemTypeId = result.ItemTypeId;
            baseItemTree.StartDate = result.StartDate;
            baseItemTree.EndDate = result.EndDate;
            baseItemTree.ItemStatusId = result.ItemStatusId;
            baseItemTree.OwnerTypeId = result.OwnerTypeId;
            baseItemTree.OwnerId = result.OwnerId;
            baseItemTree.ItemOrder = result.ItemOrder;

            return baseItemTree;
        }

        public IEnumerable<TreeBaseItemRelation> GetItemRelationTreeChildren(Guid parentId)
        {
            using IDbConnection connection = new SqlConnection(connectionString);

            string sql = @"SELECT Id,
                                  ParentId,
                                  Name,
                                  Image,
                                  ItemTypeId,
                                  StartDate,
                                  EndDate,
                                  ItemStatusId,
                                  OwnerTypeId,
                                  OwnerId,
                                  ItemOrder
                             FROM BASEITEMRELATION
                         WHERE ParentId = @parentId";

            var results = connection.Query(sql, new { ParentId = parentId });

            List<TreeBaseItemRelation> children = new List<TreeBaseItemRelation>();

            foreach (var result in results)
            {
                TreeBaseItemRelation itemTree = new TreeBaseItemRelation
                {
                    Id = result.Id,
                    ParentId = result.ParentId,
                    Name = result.Name,
                    Image = result.Image,
                    ItemTypeId = result.ItemTypeId,
                    StartDate = result.StartDate,
                    EndDate = result.EndDate,
                    ItemStatusId = result.ItemStatusId,
                    OwnerTypeId = result.OwnerTypeId,
                    OwnerId = result.OwnerId,
                    ItemOrder = result.ItemOrder
                };

                children.Add(itemTree);
            }

            return children;
        }

        #endregion ItemRelationTree
    }
}