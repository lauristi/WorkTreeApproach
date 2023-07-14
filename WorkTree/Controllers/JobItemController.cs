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
    public class JobItemController : ControllerBase
    {
        private readonly ILogger<JobItemController> _logger;
        private readonly IMapper _mapper;

        private readonly IJobItemBLL _jobItemBLL;

        public JobItemController(IJobItemBLL jobItemBLL,
                                 IMapper mapper,
                                 ILogger<JobItemController> logger)

        {
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //-----------------------------------
            _jobItemBLL = jobItemBLL ?? throw new ArgumentNullException(nameof(jobItemBLL));
        }

        #region JobItem

        [HttpGet, Route("")]
        public async Task<ActionResult<IEnumerable<JobItemResponseDTO>>> JobItemGetAllAsync()
        {
            var jobItems = await _jobItemBLL.GetAll();

            if (jobItems == null)
                return new ObjectResult(Results.NotFound());

            var jobItemsResponseDTO = _mapper.Map<IEnumerable<JobItemResponseDTO>>(jobItems);
            return new ObjectResult(jobItemsResponseDTO);
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<ActionResult<JobItemResponseDTO>> JobItemGetAsync([FromRoute] Guid id)
        {
            var jobItem = await _jobItemBLL.Get(id);

            if (jobItem == null)
                return new ObjectResult(Results.NotFound());

            var jobItemResponseDTO = _mapper.Map<JobItemResponseDTO>(jobItem);
            return new ObjectResult(jobItemResponseDTO);
        }

        [HttpPost, Route("")]
        public IActionResult JobItemPost(JobItemRequestDTO jobItemRequestDTO)
        {
            JobItem jobItem = _mapper.Map<JobItem>(jobItemRequestDTO);

            //if (!jobItem.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            var newId = _jobItemBLL.Insert(jobItem);

            return new ObjectResult(Results.Created($"/jobitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("{id:guid}")]
        public IActionResult JobItemPut([FromRoute] Guid id,
                                                JobItemRequestDTO jobItemRequestDTO)
        {
            JobItem jobItem = _mapper.Map<JobItem>(jobItemRequestDTO);
            jobItem.Id = id;

            //if (!jobItem.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            _jobItemBLL.Update(jobItem);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete, Route("{id:guid}")]
        public IActionResult JobItemDelete([FromRoute] Guid id)
        {
            _jobItemBLL.Delete(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion JobItem

        #region JobItemChild

        [HttpGet, Route("child")]
        public async Task<ActionResult<IEnumerable<JobItemChildResponseDTO>>> JobItemChildGetAllAsync()
        {
            var jobItemChilds = await _jobItemBLL.GetAllChild();

            if (jobItemChilds == null)
                return new ObjectResult(Results.NotFound());

            var jobItemChildsResponseDTO = _mapper.Map<IEnumerable<JobItemChildResponseDTO>>(jobItemChilds);
            return new ObjectResult(jobItemChildsResponseDTO);
        }

        [HttpGet, Route("child/{id:guid}")]
        public async Task<ActionResult<JobItemChildResponseDTO>> JobItemChildGetAsync([FromRoute] Guid id)
        {
            var jobItemChild = await _jobItemBLL.GetChild(id);

            if (jobItemChild == null)
                return new ObjectResult(Results.NotFound());

            var jobItemChildResponseDTO = _mapper.Map<JobItemChildResponseDTO>(jobItemChild);
            return new ObjectResult(jobItemChildResponseDTO);
        }

        [HttpPost, Route("child")]
        public IActionResult JobItemChildPost(JobItemChildRequestDTO jobItemChildRequestDTO)
        {
            JobItemChild jobItemChild = _mapper.Map<JobItemChild>(jobItemChildRequestDTO);

            //if (!jobItemChild.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            var newId = _jobItemBLL.InsertChild(jobItemChild);

            return new ObjectResult(Results.Created($"/jobitem/{newId}", newId))
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [HttpPut, Route("child/{id:guid}")]
        public IActionResult JobItemChildPut([FromRoute] Guid id,
                                                JobItemChildRequestDTO jobItemChildRequestDTO)
        {
            JobItemChild jobItemChild = _mapper.Map<JobItemChild>(jobItemChildRequestDTO);
            jobItemChild.Id = id;

            //if (!jobItemChild.IsValid){
            //    return new ObjectResult(Results.ValidationProblem(category.Notifications.ConvertToErrorDetails()))
            //    {
            //        StatusCode = StatusCodes.Status400BadRequest
            //    };
            //}

            _jobItemBLL.UpdateChild(jobItemChild);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        [HttpDelete, Route("child/{id:guid}")]
        public IActionResult JobItemChildDelete([FromRoute] Guid id)
        {
            _jobItemBLL.DeleteChild(id);

            return new ObjectResult(Results.Ok())
            {
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion JobItemChild
    }
}