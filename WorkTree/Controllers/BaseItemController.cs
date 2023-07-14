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
            baseItem.Id = id;

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

        #region BaseItemChild

        [HttpGet, Route("child")]
        public async Task<ActionResult<IEnumerable<BaseItemChildResponseDTO>>> BaseItemChildGetAllAsync()
        {
            var baseItemChilds = await _baseItemBLL.GetAllChild();

            if (baseItemChilds == null)
                return new ObjectResult(Results.NotFound());

            var baseItemChildsResponseDTO = _mapper.Map<IEnumerable<BaseItemChildResponseDTO>>(baseItemChilds);
            return new ObjectResult(baseItemChildsResponseDTO);
        }

        [HttpGet, Route("child/{id:guid}")]
        public async Task<ActionResult<BaseItemChildResponseDTO>> BaseItemChildGetAsync([FromRoute] Guid id)
        {
            var baseItemChild = await _baseItemBLL.GetChild(id);

            if (baseItemChild == null)
                return new ObjectResult(Results.NotFound());

            var baseItemChildResponseDTO = _mapper.Map<BaseItemChildResponseDTO>(baseItemChild);
            return new ObjectResult(baseItemChildResponseDTO);
        }

        [HttpPost, Route("child")]
        public IActionResult BaseItemChildPost(BaseItemChildRequestDTO baseItemChildRequestDTO)
        {
            BaseItemChild baseItemChild = _mapper.Map<BaseItemChild>(baseItemChildRequestDTO);

            //if (!baseItemChild.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            var newId = _baseItemBLL.InsertChild(baseItemChild);

            return new ObjectResult(Results.Created($"/baseitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("child/{id:guid}")]
        public IActionResult BaseItemChildPut([FromRoute] Guid id,
                                                BaseItemChildRequestDTO baseItemChildRequestDTO)
        {
            BaseItemChild baseItemChild = _mapper.Map<BaseItemChild>(baseItemChildRequestDTO);
            baseItemChild.Id = id;

            //if (!baseItemChild.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            _baseItemBLL.UpdateChild(baseItemChild);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete, Route("child/{id:guid}")]
        public IActionResult BaseItemChildDelete([FromRoute] Guid id)
        {
            _baseItemBLL.DeleteChild(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion BaseItemChild
    }
}