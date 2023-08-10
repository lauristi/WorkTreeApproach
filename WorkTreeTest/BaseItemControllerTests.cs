using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WorkTree.Business.Interface;
using WorkTree.Controllers;
using WorkTree.Database.DTO.Request;
using WorkTree.Database.DTO.Response;
using WorkTree.Database.Models.Tree;

namespace WorkTree.Tests
{
    public class BaseItemControllerTests
    {
        // Mocks para injeção de dependência
        private readonly Mock<ILogger<BaseItemController>> _loggerMock;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IBaseItemBLL> _baseItemBLLMock;

        // Controlador que será testado
        private readonly BaseItemController _controller;

        public BaseItemControllerTests()
        {
            // Configuração dos mocks
            _loggerMock = new Mock<ILogger<BaseItemController>>();
            _mapperMock = new Mock<IMapper>();
            _baseItemBLLMock = new Mock<IBaseItemBLL>();

            // Instanciando o controlador com os mocks
            _controller = new BaseItemController(_baseItemBLLMock.Object,
                                                 _mapperMock.Object,
                                                 _loggerMock.Object);
        }

        [Fact]
        public void TreeBuildShouldReturnTreeBaseItemRelationResponseDTO()
        {
            #region Arrange

            var treeBuilderOptionsRequestDTO = new TreeBuilderOptionsRequestDTO
            {
                ReferenceItemId = Guid.NewGuid(),
                BuildAscendentTree = true,
                IncludeAllAscendentChildren = true
            };

            // Mapeamento das opções de construção da árvore
            var treeBuilderOptions = new TreeBuilderOptions
            {
                ReferenceItemId = treeBuilderOptionsRequestDTO.ReferenceItemId,
                BuildAscendentTree = treeBuilderOptionsRequestDTO.BuildAscendentTree,
                IncludeAllAscendentChildren = treeBuilderOptionsRequestDTO.IncludeAllAscendentChildren
            };

            // Dados simulados da árvore
            var mockTreeBaseItemRelations = new TreeBaseItemRelation
            {
                Id = Guid.NewGuid(),
                Name = "Root Item",
                Children = new List<TreeBaseItemRelation>
                {
                    new TreeBaseItemRelation
                    {
                        Id = Guid.NewGuid(),
                        Name = "Child Item 1"
                    },
                    new TreeBaseItemRelation
                    {
                        Id = Guid.NewGuid(),
                        Name = "Child Item 2"
                    }
                }
            };

            // Dados esperados no DTO de resposta
            var mockTreeBaseItemRelationsResponseDTO = new TreeBaseItemRelationResponseDTO
            {
                Id = mockTreeBaseItemRelations.Id,
                Name = mockTreeBaseItemRelations.Name,
                Children = new List<TreeBaseItemRelationResponseDTO>
                {
                    new TreeBaseItemRelationResponseDTO
                    {
                        Id = mockTreeBaseItemRelations.Children[0].Id,
                        Name = mockTreeBaseItemRelations.Children[0].Name
                    },
                    new TreeBaseItemRelationResponseDTO
                    {
                        Id = mockTreeBaseItemRelations.Children[1].Id,
                        Name = mockTreeBaseItemRelations.Children[1].Name
                    }
                }
            };

            // Configurando comportamento dos mocks
            _mapperMock.Setup(m => m.Map<TreeBuilderOptions>(treeBuilderOptionsRequestDTO)).Returns(treeBuilderOptions);
            _baseItemBLLMock.Setup(b => b.TreeBuilder(treeBuilderOptions)).Returns(mockTreeBaseItemRelations);
            _mapperMock.Setup(m => m.Map<TreeBaseItemRelationResponseDTO>(mockTreeBaseItemRelations)).Returns(mockTreeBaseItemRelationsResponseDTO);

            #endregion Arrange

            #region Act

            var result = _controller.BaseItemRelationTreeBuild(treeBuilderOptionsRequestDTO);

            #endregion Act

            #region Assert

            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            var returnedTreeBaseItemRelations = Assert.IsType<TreeBaseItemRelationResponseDTO>(objectResult.Value);

            // Verificações específicas do resultado retornado
            Assert.Equal(mockTreeBaseItemRelationsResponseDTO.Id, returnedTreeBaseItemRelations.Id);
            Assert.Equal(mockTreeBaseItemRelationsResponseDTO.Name, returnedTreeBaseItemRelations.Name);
            Assert.Equal(mockTreeBaseItemRelationsResponseDTO.Children.Count, returnedTreeBaseItemRelations.Children.Count);

            #endregion Assert
        }
    }
}