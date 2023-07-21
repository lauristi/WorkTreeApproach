using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkTree.Business.Interface;
using WorkTree.Database.DTO.Request;
using WorkTree.Database.DTO.Response;
using WorkTree.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseItemController : ControllerBase
    {
        private readonly ILogger<BaseItemController> _logger;
        private readonly IMapper _mapper;

        private readonly IBaseItemBLL _baseItemBLL;

        public BaseItemController(IBaseItemBLL baseItemBLL,
                                  IMapper mapper,
                                  ILogger<BaseItemController> logger)
        {
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //-----------------------------------
            _baseItemBLL = baseItemBLL ?? throw new ArgumentNullException(nameof(baseItemBLL));
        }

        #region BaseItem

        [HttpGet, Route("")]
        public async Task<ActionResult<IEnumerable<BaseItemResponseDTO>>> BaseItemGetAllAsync()
        {
            var baseItems = await _baseItemBLL.GetAll();

            if (baseItems == null)
                return new ObjectResult(Results.NotFound());

            var baseItemsResponseDTO = _mapper.Map<IEnumerable<BaseItemResponseDTO>>(baseItems);
            return new ObjectResult(baseItemsResponseDTO);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<ActionResult<BaseItemResponseDTO>> BaseItemGetAsync([FromRoute] Guid id)
        {
            var baseItem = await _baseItemBLL.Get(id);

            if (baseItem == null)
                return new ObjectResult(Results.NotFound());

            var baseItemResponseDTO = _mapper.Map<BaseItemResponseDTO>(baseItem);
            return new ObjectResult(baseItemResponseDTO);
        }

        [HttpPost, Route("")]
        public IActionResult BaseItemPost(BaseItemRequestDTO baseItemRequestDTO)
        {
            BaseItem baseItem = _mapper.Map<BaseItem>(baseItemRequestDTO);

            //if (!baseItem.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            var newId = _baseItemBLL.Insert(baseItem);

            return new ObjectResult(Results.Created($"/baseitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("{id:guid}")]
        public IActionResult BaseItemPut([FromRoute] Guid id,
                                                BaseItemRequestDTO baseItemRequestDTO)
        {
            BaseItem baseItem = _mapper.Map<BaseItem>(baseItemRequestDTO);

            //if (!baseItem.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            _baseItemBLL.Update(baseItem);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete, Route("{id:guid}")]
        public IActionResult BaseItemDelete([FromRoute] Guid id)
        {
            _baseItemBLL.Delete(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion BaseItem

        #region BaseItemRelation

        [HttpGet, Route("{id:guid}/itemRelations")]
        public async Task<ActionResult<IEnumerable<BaseItemRelationResponseDTO>>> BaseItemRelationGetAllAsync([FromRoute] Guid id)
        {
            var baseItemRelations = await _baseItemBLL.GetAllItemRelation(id);

            if (baseItemRelations == null)
                return new ObjectResult(Results.NotFound());

            var baseItemRelationsResponseDTO = _mapper.Map<IEnumerable<BaseItemRelationResponseDTO>>(baseItemRelations);
            return new ObjectResult(baseItemRelationsResponseDTO);
        }

        [HttpGet, Route("itemRelation/{id:guid}")]
        public async Task<ActionResult<BaseItemRelationResponseDTO>> BaseItemRelationGetAsync([FromRoute] Guid id)
        {
            var baseItemRelation = await _baseItemBLL.GetItemRelation(id);

            if (baseItemRelation == null)
                return new ObjectResult(Results.NotFound());

            var baseItemRelationResponseDTO = _mapper.Map<BaseItemRelationResponseDTO>(baseItemRelation);
            return new ObjectResult(baseItemRelationResponseDTO);
        }

        [HttpPost, Route("itemRelation")]
        public IActionResult BaseItemRelationPost(BaseItemRelationRequestDTO baseItemRelationRequestDTO)
        {
            BaseItemRelation baseItemRelation = _mapper.Map<BaseItemRelation>(baseItemRelationRequestDTO);

            //if (!baseItemRelation.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            var newId = _baseItemBLL.InsertItemRelation(baseItemRelation);

            return new ObjectResult(Results.Created($"/baseitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("itemRelation/{id:guid}")]
        public IActionResult BaseItemRelationPut([FromRoute] Guid id,
                                                BaseItemRelationRequestDTO baseItemRelationRequestDTO)
        {
            BaseItemRelation baseItemRelation = _mapper.Map<BaseItemRelation>(baseItemRelationRequestDTO);

            //if (!baseItemRelation.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            _baseItemBLL.UpdateItemRelation(baseItemRelation);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete, Route("itemRelation/{id:guid}")]
        public IActionResult BaseItemRelationDelete([FromRoute] Guid id)
        {
            _baseItemBLL.DeleteItemRelation(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion BaseItemRelation
    }
}