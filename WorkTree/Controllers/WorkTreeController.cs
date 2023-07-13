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
    public class WorkTreeController : ControllerBase
    {
        private readonly ILogger<WorkTreeController> _logger;
        private readonly IMapper _mapper;

        private readonly IBaseItemBLL _baseItemBLL;
        private readonly IJobItemBLL _jobItemBLL;

        public WorkTreeController(IBaseItemBLL baseItemBLL,
                                   IJobItemBLL jobItemBLL,
                                   IMapper mapper,
                                   ILogger<WorkTreeController> logger)
        {
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _baseItemBLL = baseItemBLL ?? throw new ArgumentNullException(nameof(baseItemBLL));
            _jobItemBLL = jobItemBLL ?? throw new ArgumentNullException(nameof(jobItemBLL));
        }

        #region BaseItem

        [HttpGet, Route("baseitem")]
        public async Task<ActionResult<IEnumerable<BaseItemResponseDTO>>> BaseItemGetAllAsync()
        {
            var baseItems = await _baseItemBLL.GetAll();

            if (baseItems == null)
                return new ObjectResult(Results.NotFound());

            var baseItemsResponseDTO = _mapper.Map<IEnumerable<BaseItemResponseDTO>>(baseItems);
            return new ObjectResult(baseItemsResponseDTO);
        }

        [HttpGet, Route("baseitem/{id:guid}")]
        public async Task<ActionResult<BaseItemResponseDTO>> BaseItemGetAsync([FromRoute] Guid id)
        {
            var baseItem = await _baseItemBLL.Get(id);

            if (baseItem == null)
                return new ObjectResult(Results.NotFound());

            var baseItemResponseDTO = _mapper.Map<BaseItemResponseDTO>(baseItem);
            return new ObjectResult(baseItemResponseDTO);
        }

        [HttpPost, Route("baseitem")]
        public Task<IActionResult> BaseItemPost(BaseItemRequestDTO baseItemRequestDTO)
        {
            BaseItem baseItem = _mapper.Map<BaseItem>(baseItemRequestDTO);

            //if (!baseItem.IsValid)
            //{
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            Guid newId = _baseItemBLL.Insert(baseItem);

            return new ObjectResult(Results.Created($"/baseitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("baseitem/{id:guid}")]
        public IActionResult BaseItemPut([FromRoute] Guid id,
                                                BaseItemRequestDTO baseItemRequestDTO)
        {
            BaseItem baseItem = _mapper.Map<BaseItem>(baseItemRequestDTO);
            baseItem.Id = id;

            //if (!baseItem.IsValid)
            //{
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
        public IActionResult CBaseItemDelete([FromRoute] Guid id)
        {
            _baseItemBLL.Delete(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }

    #endregion BaseItem
}